using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERMUIKit.Components;
using Microsoft.AspNetCore.Components;

namespace SME.UnifiedPlatform.WebCore.UIComponents.Utility
{
    public partial class Wizard : ComponentBase
    {
        /// <summary>
        /// List of <see cref="WizardStep"/> added to the Wizard
        /// </summary>
        protected internal List<WizardStep> Steps = new List<WizardStep>();

        /// <summary>
        /// The control Id
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public string ProgressPercent
        {
            get
            {
                int v = 0;
                if (Steps is not null && Steps.Count > 0)
                {
                    int activeIndex = 0;
                    for (int i = 0; i < Steps.Count; i++)
                    {
                        if (Steps[i].IsEnabled == true)
                        {
                            activeIndex = i;
                        }
                    }
                    v = (100 / Steps.Count) * (activeIndex + 1);
                }
                return v.ToString() + "%";
            }
            set
            {
                ProgressPercent = value;
            }
        }
        /// <summary>
        /// The ChildContent container for <see cref="WizardStep"/>
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The Active <see cref="WizardStep"/>
        /// </summary>
        [Parameter]
        public WizardStep ActiveStep { get; set; }

        /// <summary>
        /// The Index number of the <see cref="ActiveStep"/>
        /// </summary>
        [Parameter]
        public int ActiveStepIx { get; set; }

        /// <summary>
        /// Determines whether the Wizard is in the last step
        /// </summary>

        public bool IsLastStep { get; set; }

        /// <summary>
        /// Sets the <see cref="ActiveStep"/> to the previous Index
        /// </summary>

        protected internal void GoBack()
        {
            if (ActiveStepIx > 0)
                SetActive(Steps[ActiveStepIx - 1]);
        }

        /// <summary>
        /// Sets the <see cref="ActiveStep"/> to the next Index
        /// </summary>
        protected internal void GoNext()
        {

            if (ActiveStepIx < Steps.Count - 1)
            {
                WizardStep step = Steps[(Steps.IndexOf(ActiveStep) + 1)];
                if (step.IsEnabled)
                {
                    SetActive(Steps[(Steps.IndexOf(ActiveStep) + 1)]);
                }
            }
        }

        /// <summary>
        /// Populates the <see cref="ActiveStep"/> the Sets the passed in <see cref="WizardStep"/> instance as the
        /// </summary>
        /// <param name="step">The WizardStep</param>

        protected internal void SetActive(WizardStep step)
        {
            if (step.IsEnabled)
            {

                ActiveStep = step ?? throw new ArgumentNullException(nameof(step));

                ActiveStepIx = StepsIndex(step);
                if (ActiveStepIx == Steps.Count - 1)
                    IsLastStep = true;
                else
                    IsLastStep = false;
            }
        }

        /// <summary>
        /// Retrieves the index of the current <see cref="WizardStep"/> in the Step List
        /// </summary>
        /// <param name="step">The WizardStep</param>
        /// <returns></returns>
        public int StepsIndex(WizardStep step) => StepsIndexInternal(step);
        protected int StepsIndexInternal(WizardStep step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));

            return Steps.IndexOf(step);
        }
        /// <summary>
        /// Adds a <see cref="WizardStep"/> to the WizardSteps list
        /// </summary>
        /// <param name="step"></param>
        protected internal void AddStep(WizardStep step)
        {
            Steps.Add(step);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SetActive(Steps[ActiveStepIx]);
                StateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            //For Start Up Card
            if (ActiveStepIx == 1 || ActiveStepIx == 2 || ActiveStepIx == 3)
            {
                SetActive(Steps[ActiveStepIx]);
                StateHasChanged();
            }
        }

        public void EnableStep(int stepIndex)
        {
            Steps[stepIndex].IsEnabled = true;
        }

        public void DisableStep(int stepIndex)
        {
            Steps[stepIndex].IsEnabled = false;
        }
    }
}
