using Integra.Core;
using Integra.Database;
using Integra.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Integra
{
    public enum SessionTypes : byte
    {
        StudioSet = 0x00,
        Tone      = 0x01,
        MFX       = 0x02
    }

    public class SessionData: IntegraDataTemplate<SessionData>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SessionTypes Type { get; set; }
        public string Description { get; set; }
    }

    public class Session : IntegraBase<Session>
    {
        public Session() 
        {
            SessionID = DataAccess.GetNextID(this);

            Debug.Print($"[{nameof(Session)}] ID: {SessionID}");

            //_Data = new List<SessionData>();

            //DataAccess.Select(this);
            Sessions = DataAccess.Select(this, new SessionData());
        }

        private List<SessionData> _Data;

        public List<SessionData> Sessions 
        {
            get { return _Data; }

            private set
            {
                _Data = value;
                NotifyPropertyChanged();
            }
        }

        // TODO: Associate virtual slots used by the session

        public int SessionID { get; set; }
        public new string Name { get; set; } = string.Empty;
        public SessionTypes Type { get; private set; } = SessionTypes.StudioSet;
        public string Description { get; set; } = string.Empty;

        protected override void InitializeParameterCache()
        {
            if (_TypeCacheInitialized)
            {
                return;
            }

            _TypeCache.Add(new SQLData(0, typeof(string), 0, nameof(Name)));
            _TypeCache.Add(new SQLData(0, typeof(byte), 0, nameof(Type)));
            _TypeCache.Add(new SQLData(0, typeof(string), 0, nameof(Description)));
        }

        public override void Load(int id)
        {
            DataAccess.Load(this, 1);

            foreach (var item in Parameters)
            {
                GetType().GetProperty(item.Name).SetValue(this, item.Value);
            }

            SessionID = id;
            DebugPrint();
        }

        public override void Save()
        {
            // TODO: Overwrite or new session ID
            List<SQLParameter> parameters = new List<SQLParameter>();

            parameters.Add(new SQLParameter(typeof(string), nameof(Name), Name));
            parameters.Add(new SQLParameter(typeof(byte), nameof(Type), (byte)Type));
            parameters.Add(new SQLParameter(typeof(string), nameof(Description), Description));

            DataAccess.Save(this, parameters, false, false);

            Device.Instance.StudioSet.Save();

            SessionID = DataAccess.GetNextID(this);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Truncate()
        {
            DataAccess.Truncate(this);

            Device.Instance.StudioSet.Truncate();

            SessionID = DataAccess.GetNextID(this);

            DebugPrint();
        }

        public IEnumerable<SessionTypes> SessionType
        {
            get { return Enum.GetValues(typeof(SessionTypes)).Cast<SessionTypes>(); }
        }
    }

}
