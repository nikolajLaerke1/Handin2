using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2
{
    public interface IDoor
    {
        public void LockDoor();

        public void UnlockDoor();

        public void OnDoorOpen();

        public void OnDoorClose();
    }
}
