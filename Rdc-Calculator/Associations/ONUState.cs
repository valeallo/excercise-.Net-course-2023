﻿using Associations.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    internal class ONUState : State, IONU
    {
        ONU _onu;
       public ONUState  (string Name, ONU Onu) : base (Name)
        {
        
            _onu = Onu;
        
        }

    }
}
