using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Transactions
{
    public class Transaction : IDisposable
    {
        private Action TransactionAction { get; set; }
        public bool committed { get; private set; }
        public bool Aborted { get; private set; }

        private object SyncRoot { get; set; }

        bool disposing;

        public Transaction(Action action)
        {
            TransactionAction = action;
            disposing = false;
        }

        private void Commit()
        {
            TransactionAction.Invoke();
        }

        private void Abort()
        {
            
        }

        void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (SyncRoot)
                {
                    if (!(committed || Aborted))
                    {
                        var isInException = Marshal.GetExceptionPointers() != IntPtr.Zero || Marshal.GetExceptionCode() != 0;
                        if (!isInException)
                            Commit();
                        else
                            Abort();
                    }
                }
                //Writes = null;
            }
        }


    }
}
