using MuntersHomeTask.PageObject.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuntersHomeTask.PageObject
{
    public class MainPage
    {
        public FarmListComp FarmListComp = new();
        public ControllerListComp ControllerListComp = new();
        public FarmDetailsComp FarmDetailsComp = new();
    }
}
