# DevGenie 🧞‍♂️ - Your AI-Powered Coding Assistant

*Generate, Explain, and Review Code with Ease!*

**DevGenie** is an open-source AI-powered coding assistant built using ASP.NET C# (MVC framework). Whether you're looking to generate code in multiple languages, get detailed explanations for complex code, or receive professional feedback on your snippets, DevGenie has got you covered! 🚀

---

## ✨ Features

- **Code Generation**: Generate code in multiple programming languages from simple descriptions.
- **Code Explanation**: Get detailed, easy-to-understand explanations for complex code snippets.
- **Code Review**: Receive professional feedback to improve your code quality.
- **Open-Source**: Free to use, modify, and contribute to under the MIT License.
- **Powered by Open AI**: Leverage the power of Open AI's API for intelligent code assistance.

---

## 📺 Demo Video

<!-- Replace the link below with your YouTube video embed link -->
<iframe width="560" height="315" src="https://www.youtube.com/embed/YOUR_VIDEO_ID" title="DevGenie Demo" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

---

## 🛠️ Project Structure

Here’s the structure of the DevGenie project to help you navigate the codebase:

```
DevGenie/
│
├── App_Data/
├── App_Start/
│   ├── RouteConfig.cs
│   └── WebApiConfig.cs
├── configs/
│   └── appsettings.json   # Add your Open AI API key here
├── Controllers/
│   ├── AuthController.cs
│   └── OpenAIController.cs
├── Data/
│   └── ApplicationDBContext.cs
├── Models/
├── Pages/
│   ├── AuthUtility.cs
│   ├── DASHBOARD.aspx
│   ├── DASHBOARD.aspx.cs
│   ├── DASHBOARD.aspx.designer.cs
│   ├── GENERATOR.aspx
│   ├── GENERATOR.aspx.cs
│   ├── HOME.aspx         # Homepage for DevGenie
│   ├── HOME.aspx.cs
│   ├── LOGIN.aspx
│   ├── LOGIN.aspx.cs
│   ├── OUTPUT.aspx
│   ├── OUTPUT.aspx.cs
│   ├── REGISTER.aspx
│   ├── REGISTER.aspx.cs
│   ├── REGISTER.aspx.designer.cs
│   ├── RESETPASSWORD.aspx
│   ├── RESETPASSWORD.aspx.cs
│   ├── RESETPASSWORD.aspx.designer.cs
│   ├── REVIEW.aspx
│   ├── REVIEW.aspx.cs
│   ├── SITE-MASTER.cs
│   ├── Scripts/
│   │   └── script.js
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── OpenAIService.cs
│   └── Styles/
│       ├── auth.css
│       ├── DASHBOARD.css
│       ├── GENERATOR.css
│       ├── HOME.css
│       ├── OUTPUT.css
│       ├── REVIEW.css
│       ├── styles.css
│       └── GLOBAL.asax
├── GLOBAL.asax.cs
├── packages.config
└── Web.config
```

---

## 🚀 Getting Started

Follow these steps to set up DevGenie on your local machine.

### Prerequisites

- **.NET Framework**: Ensure you have .NET Framework installed (version 4.7.2 or higher).
- **Visual Studio**: Recommended for running and debugging the project.
- **Open AI API Key**: You’ll need an Open AI API key to power the AI features.

### Installation Steps

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/your-username/devgenie.git
   cd devgenie
   ```

2. **Add Your Open AI API Key**  
   - Navigate to the `configs/` directory.
   - Open `appsettings.json`.
   - Add your Open AI API key in the following format:
     ```json
     {
       "OpenAI": {
         "ApiKey": "your-open-ai-api-key-here"
       }
     }
     ```

3. **Restore NuGet Packages**  
   The project uses several NuGet packages, which should automatically restore when you build the project in Visual Studio. If they don’t, manually restore them by running:
   ```bash
   dotnet restore
   ```
   Alternatively, in Visual Studio:
   - Right-click on the solution in Solution Explorer.
   - Select **"Restore NuGet Packages"**.

   **Note**: The project includes a large number of NuGet packages (listed below). You don’t need to manually install them all, as most are dependencies that will be automatically resolved. However, ensure the following key packages are installed:
   - `Microsoft.AspNet.Mvc` (for MVC framework)
   - `Newtonsoft.Json` (for JSON handling)

4. **Build and Run**  
   - Open the solution file (`DevGenie.sln`) in Visual Studio.
   - Build the solution (Ctrl+Shift+B).
   - Run the project (F5). It will launch on `localhost:62971` (or another port if configured differently).

5. **Access DevGenie**  
   Open your browser and navigate to `http://localhost:62971/Pages/HOME.aspx` to see the homepage.

---

## 📦 NuGet Packages

The project uses the following NuGet packages. Most of these are dependencies that will be automatically restored, but here’s the full list for reference:

- **Microsoft.AspNet.Mvc** (v5.3.0)
- **Microsoft.AspNet.Razor** (v3.3.0)
- **Microsoft.AspNet.Web.Optimization** (v1.1.3)
- **Microsoft.AspNet.WebApi** (v5.3.0)
- **Microsoft.AspNet.WebApi.Client** (v6.0.0)
- **Microsoft.AspNet.WebApi.Core** (v5.3.0)
- **Microsoft.AspNet.WebApi.WebHost** (v5.3.0)
- **Microsoft.AspNet.WebPages** (v3.3.0)
- **Microsoft.Bcl.AsyncInterfaces** (v9.0.3)
- **Microsoft.CodeDom.Providers.DotNetCompilerPlatform** (v4.1.0)
- **Microsoft.Extensions.Configuration** (v9.0.3)
- **Microsoft.Extensions.Configuration.Abstractions** (v9.0.3)
- **Microsoft.Extensions.Configuration.Binder** (v9.0.3)
- **Microsoft.Extensions.Configuration.FileExtensions** (v9.0.3)
- **Microsoft.Extensions.Configuration.Json** (v9.0.3)
- **Microsoft.Extensions.FileProviders.Abstractions** (v9.0.3)
- **Microsoft.Extensions.FileProviders.Physical** (v9.0.3)
- **Microsoft.Extensions.FileSystemGlobbing** (v9.0.3)
- **Microsoft.Extensions.Primitives** (v9.0.3)
- **Microsoft.Web.Infrastructure** (v2.0.0)
- **Newtonsoft.Json** (v13.0.3)
- **System.Buffers** (v4.6.1)
- **System.IO** (v4.3.0)
- **System.IO.Pipelines** (v9.0.3)
- **System.Memory** (v4.6.2)
- **System.Net.Http** (v4.3.4)
- **System.Net.WebHeaderCollection** (v4.3.0)
- **System.Numerics.Vectors** (v4.6.1)
- **System.Runtime** (v4.3.1)
- **System.Runtime.CompilerServices.Unsafe** (v6.1.1)
- **System.Security.Cryptography.Algorithms** (v4.3.1)
- **System.Security.Cryptography.Encoding** (v4.3.0)
- **System.Security.Cryptography.Primitives** (v4.3.0)
- **System.Security.Cryptography.X509Certificates** (v4.3.2)
- **System.Text.Encodings.Web** (v9.0.3)
- **System.Text.Json** (v9.0.3)
- **System.Threading.Tasks.Extensions** (v4.6.2)
- **System.ValueTuple** (v4.6.1)
- **WebGrease** (v1.6.0)

**Important**: You likely only manually installed `Microsoft.AspNet.Mvc` and `Newtonsoft.Json`. The rest are dependencies that were automatically included when you restored the packages.

---

## 📜 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details. Feel free to use, modify, and distribute this project as you see fit!

---

## 🤝 Contributing

Contributions are welcome! If you’d like to contribute to DevGenie, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit them (`git commit -m "Add your feature"`).
4. Push to your branch (`git push origin feature/your-feature`).
5. Open a Pull Request.

---

## 💡 Acknowledgments

- Built with ❤️ using ASP.NET C# and the MVC framework.
- Powered by Open AI’s API for intelligent code assistance.
- Thanks to the open-source community for the amazing tools and libraries!

---

## 📧 Contact

For any questions or feedback, feel free to reach out:  
📧 [selva.p@somaiya.edu](mailto:selva.p@somaiya.edu)  

---

Happy Coding with DevGenie! 🧞‍♂️

