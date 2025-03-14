<%@ Page Title="Home" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DevGenie.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="welcome-container">
        <h1>Welcome to DevGenie</h1>
        <p>Your AI-powered coding assistant</p>
        <div class="features">
            <div class="feature">
                <h2>Code Generation</h2>
                <p>Generate code in multiple languages from simple descriptions</p>
                <a href="Generator.aspx" class="feature-button">Try Generator</a>
            </div>
            <div class="feature">
                <h2>Code Explanation</h2>
                <p>Get detailed explanations for complex code</p>
                <a href="Output.aspx" class="feature-button">Try Explanation</a>
            </div>
            <div class="feature">
                <h2>Code Review</h2>
                <p>Get professional feedback on your code</p>
                <a href="Review.aspx" class="feature-button">Try Review</a>
            </div>
        </div>
    </div>
</asp:Content>