using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevGenie
{
    public partial class Output : Page
    {
        protected TextBox code;
        protected Label explanation;

        protected async void ExplainButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var openAIService = new OpenAIService();
                explanation.Text = await openAIService.ExplainCodeAsync(code.Text.Trim(), "any");
            }
            catch (Exception ex)
            {
                explanation.Text = $"Error: {ex.Message}";
            }
        }
    }
}