using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class CustomNumericUpDown : NumericUpDown
    {
        protected override void OnValidating(CancelEventArgs e)
        {
            if (!e.Cancel && UserEdit)
            {
                ValidateEditText();
            }
            base.OnValidating(e);
        }
    }
}
