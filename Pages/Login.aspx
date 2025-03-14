<%@ Page Title="Login" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DevGenie.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Styles/style.css">
    <h2>Login</h2>
    <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username"></asp:TextBox><br />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox><br />
    <asp:Button ID="btnLogin" runat="server" Text="Login"/>
</asp:Content>
