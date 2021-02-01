using Integra.Core;
using Integra.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Integra
{
    public enum SessionTypes : byte
    {
        StudioSet = 0x00,
        Tone      = 0x01,
        MFX       = 0x02
    }

    //public class SessionData : IntegraDataTemplate<SessionData>
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public SessionTypes Type { get; set; }
    //    public string Description { get; set; }
    //}

    /// <summary>
    /// 
    /// </summary>
    public class Session : IntegraBase<Session>
    {
        public Session() 
        {
            ID = DataAccess.GetNextID(this);

            Debug.Print($"[{nameof(Session)}] {ID}");
        }
        private ObservableCollection<Session> _Sessions = new ObservableCollection<Session>();
        public virtual ObservableCollection<Session> Sessions
        {
            get { return DataAccess.SelectAll<Session>(); ; }
            set
            {
                _Sessions = value;
                NotifyPropertyChanged();
            }
        }

        // TODO: Associate virtual slots used by the session
        private int _ID;

        public new int ID
        {
            get { return _ID; }
            set 
            {
                if (_ID != value)
                {
                    if (value < 0)
                        _ID = DataAccess.GetNextID(this);
                    else
                        _ID = value;

                    NotifyPropertyChanged();
                    Debug.Print($"[{nameof(Session)}] ID: {_ID}");
                }
            }
        }

        public new string Name { get; set; } = string.Empty;
        public SessionTypes Type { get; private set; } = SessionTypes.StudioSet;
        public string Description { get; set; } = string.Empty;

        public override void Select(int id)
        {
            int result = DataAccess.Select(this, id);

            if (result != 0)
            {
                ID = id;
                //DebugPrint();
                DataAccess.Select(Device.Instance.StudioSet, ID);
            }
        }

        public override void Update()
        {
            //DataAccess.Update(this);
            DataAccess.Update(Device.Instance.StudioSet);
        }

        public override void Insert()
        {
            DataAccess.Insert(this);
            Device.Instance.StudioSet.Insert();

            ID = DataAccess.GetNextID(this);

            NotifyPropertyChanged(nameof(Sessions));
        }

        public override void Delete()
        {
            DataAccess.Delete(this);
            Device.Instance.StudioSet.Delete();

            ID = DataAccess.GetNextID(this);

            NotifyPropertyChanged(nameof(Sessions));
        }

        public override void Truncate()
        {
            DataAccess.Truncate(this);
            Device.Instance.StudioSet.Truncate();

            ID = DataAccess.GetNextID(this);

            NotifyPropertyChanged(nameof(Sessions));
        }

        public virtual IEnumerable<SessionTypes> SessionType
        {
            get { return Enum.GetValues(typeof(SessionTypes)).Cast<SessionTypes>(); }
        }
    }
}
