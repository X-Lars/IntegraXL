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

    public class SessionData : IntegraDataTemplate<SessionData>
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
            ID = DataAccess.GetNextID(this);

            Debug.Print($"[{nameof(Session)}] ID: {ID}");

            Sessions = DataAccess.SelectAll(this, new SessionData());

            IntegraCache.GetTemplate<StudioSet>();
            //Sessions = DataAccess.Select<Session>();
        }

        private List<SessionData> _Data;

        public virtual List<SessionData> Sessions 
        {
            get { return _Data; }

            private set
            {
                _Data = value;
                NotifyPropertyChanged();
            }
        }

        // TODO: Associate virtual slots used by the session

        public new int ID { get; set; }
        public new string Name { get; set; } = string.Empty;
        public SessionTypes Type { get; private set; } = SessionTypes.StudioSet;
        public string Description { get; set; } = string.Empty;

        public override void Select(int id)
        {
            int result = DataAccess.Select(this, 1);

            if (result != 0)
            {
                ID = id;
                DebugPrint();
                DataAccess.Select(Device.Instance.StudioSet, 11);
            }
        }

        public override void Insert()
        {
            // TODO: Overwrite or new session ID
            //List<SQLParameter> parameters = new List<SQLParameter>();

            //parameters.Add(new SQLParameter(typeof(string), nameof(Name), Name));
            //parameters.Add(new SQLParameter(typeof(byte), nameof(Type), (byte)Type));
            //parameters.Add(new SQLParameter(typeof(string), nameof(Description), Description));

            DataAccess.Insert(this);

            Device.Instance.StudioSet.Insert();

            ID = DataAccess.GetNextID(this);
        }

        public override void Delete()
        {
            DataAccess.Delete(this);
            Device.Instance.StudioSet.Delete();
            ID = DataAccess.GetNextID(this);

            DebugPrint();
        }

        public override void Truncate()
        {
            DataAccess.Truncate(this);

            Device.Instance.StudioSet.Truncate();

            ID = DataAccess.GetNextID(this);

            DebugPrint();
        }

        public virtual IEnumerable<SessionTypes> SessionType
        {
            get { return Enum.GetValues(typeof(SessionTypes)).Cast<SessionTypes>(); }
        }
    }

}
