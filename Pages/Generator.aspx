<%@ Page Title="Code Generator" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Generator.aspx.cs" Inherits="DevGenie.Generator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Code Generator
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Generator.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <h2>Code Generator</h2>
        <p>Describe what you want to build and we'll generate the code for you.</p>
        
        <div class="form-group">
            <label for="language">Language:</label>
            <asp:DropDownList ID="language" runat="server" CssClass="form-control">
                <asp:ListItem Value="C#">C#</asp:ListItem>
                <asp:ListItem Value="Python">Python</asp:ListItem>
                <asp:ListItem Value="JavaScript">JavaScript</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div class="form-group">
            <label for="description">Description:</label>
            <asp:TextBox ID="description" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Button ID="GenerateButton" runat="server" Text="Generate" OnClick="GenerateButton_Click" CssClass="btn" />
        </div>
        
        <div class="form-group">
            <label for="generatedCode">Generated Code:</label>
            <asp:TextBox ID="generatedCode" runat="server" TextMode="MultiLine" Rows="10" ReadOnly="true" CssClass="form-control code-output"></asp:TextBox>
        </div>
    </div>
</asp:Content>