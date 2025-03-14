using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevGenie
{
    public partial class Generator : Page
    {
        protected DropDownList language;
        protected TextBox description;
        protected TextBox generatedCode;

        protected async void GenerateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var aiService = new OpenAIService();
                string lang = language.SelectedValue;
                string prompt = description.Text.Trim();

                if (string.IsNullOrWhiteSpace(prompt))
                {
                    generatedCode.Text = "Please enter a valid description.";
                    return;
                }

                generatedCode.Text = await aiService.GenerateCodeAsync(prompt, lang);
            }
            catch (Exception ex)
            {
                generatedCode.Text = $"Error: {ex.Message}";
            }
        }
    }
}