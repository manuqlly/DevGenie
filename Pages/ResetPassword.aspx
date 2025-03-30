<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DevGenie.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Reset Password
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-container">
        <h2>Reset Password</h2>
        <p>Enter your email address to receive a password reset link.</p>
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" />
        </div>
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" />
        <div class="form-group">
            <asp:Button ID="btnReset" runat="server" Text="Reset Password" CssClass="btn" OnClick="btnReset_Click" />
        </div>
        <div class="auth-links">
            <a href="Login.aspx">Back to Login</a>
        </div>
    </div>
</asp:Content>