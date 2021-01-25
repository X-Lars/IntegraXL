using MidiXL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Integra.Core.Interfaces;

namespace Integra.Core
{
    /// <summary>
    /// Base collection for INTEGRA-7 per part data structures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IntegraBasePartial<T> : ObservableCollection<T>, IIntegraDataClass  where T : IntegraBase<T>, IIntegraPartial
    {
        private readonly SynchronizationContext _Context;

        #region Constructor

        public IntegraBasePartial(IntegraAddress address, IntegraRequest request)
        {

            _Context = Device.UIContext;

            for (int i = 0; i < 16; i++)
            {
                T item = Activator.CreateInstance<T>();

                item.Address = (uint)address + (uint)(i << 8);
                item.Requests.Add(request);

                item.Initialize();

                Add(item);

                //Debug.Print($"[{nameof(IntegraBasePartial<T>)}] New: {typeof(T).Name}[{item.Address.ToString()}]");
            }
        }

        #endregion

        #region IIntegraDataClass

        public void Load(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Load(id);
            }
        }

        public void Save()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                // TODO: Call Save
                Items[i].Save();
            }
        }

        public void Truncate()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Truncate();
            }
        }

        #endregion
    }
}
