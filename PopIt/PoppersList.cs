using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopIt
{
    public class PoppersList
    {
        private Popper[] _poppers = null;

        // Indexer
        public Popper this[int index]
        {
            get { return _poppers != null ? _poppers[index] : null; }
        }

        // Count of poppers
        public int Length
        {
            get { return _poppers != null ? _poppers.Length : 0; }
        }

        // Add a popper
        public int Add(Popper popper)
        {
            // Create or resize our poppers array
            if (_poppers == null)
                _poppers = new Popper[1];
            else
                Array.Resize(ref _poppers, _poppers.Length + 1);

            _poppers[_poppers.Length - 1] = popper;
            return _poppers.Length;
        }

        public void Delete(Popper popper)
        {
            if (_poppers != null)
            {
                if (_poppers.Length > 1)
                {
                    Array.Resize(ref _poppers, _poppers.Length - 1);
                }

                else
                    Clear();

            }
        }

        // Clear all poppers
        public void Clear()
        {
            if (_poppers != null)
            {
                // Remove reference to popper objects, then undimension poppers array
                Array.Clear(_poppers, 0, _poppers.Length);
                _poppers = null;
            }
        }
    }
}