using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Utils.Tests
{
    internal abstract class BaseTest
    {
        public abstract KeyCode KeyCode { get; }

        public abstract void Execute();
    }
}
