using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERMUIKit
{
    public enum BSColorStyle
    {
        normal
        , primary
        , secondary
        , success
        , danger
        , info
        , warning
    }

    public enum RuleStatusType
    {
        Success,
        Failure,
        Normal
    }

    public enum StageBorderSize
    {
        None,
        Thin,
        Normal,
        Thick
    }

    public enum CanvasPositions
    {
        [field: Description("offcanvas-start")]
        start,
        [field: Description("offcanvas-end")]
        end,
        [field: Description("offcanvas-top")]
        top,
        [field: Description("offcanvas-bottom")]
        bottom,
    }

    public enum InputType
    {
        Text,
        Number,
        MultiLine,
        CheckBox
    }

    public enum ScreenType
    {
        Horizontal,
        Vertical
    }

}
