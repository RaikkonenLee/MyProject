<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LoginPage.Layouts.Login.Login" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登入</title>
    <style>
        body {
            margin: 0px;
            padding: 0px;
            border: 0px;
        }
        .g-bd2 {
            margin: 0px;
        }
 
        .g-sd2 {
            position: relative;
            width: 800px;
            margin: 0 auto;
        }
 
        .g-sd2 > div {
            position: fixed;
            height: 100%;
            background-color: #EDFAFB !important;
            width:800px;
            padding-left:20px;
            padding-top:100px;
        }
        .g-sd2 tr {
            line-height:60px;
            height:60px;
        }
        .error-message {
            color: #ff0000;
        }
    </style>
</head>
<body>
    <div class="g-bd2">
        <div class="g-sd2">
            <div>
                <form id="form1" runat="server" class="loginForm">
                    <asp:Login
                        ID="signInControl"
                        Style="width: 500px"
                        FailureText="帳號或密碼錯誤"
                        PasswordRequiredErrorMessage="密碼必須輸入"
                        UserNameRequiredErrorMessage="帳號必須輸入"
                        MembershipProvider="FBAMembershipProvider"
                        runat="server"
                        DisplayRememberMe="false"
                        TextBoxStyle-Width="400px"
                        RememberMeSet="false"
                        UserNameLabelText="帳號: "
                        TextLayout="TextOnLeft"
                        PasswordLabelText="密碼: "
                        LabelStyle-Font-Bold="false"
                        LabelStyle-Font-Size="Large"
                        LabelStyle-ForeColor=""
                        LabelStyle-Font-Names="細明體"
                        CheckBoxStyle-Font-Bold="false"
                        CheckBoxStyle-Font-Names="細明體"
                        CheckBoxStyle-ForeColor=""
                        CheckBoxStyle-Font-Size="Large"
                        FailureTextStyle-Wrap="true"
                        FailureTextStyle-Font-Names="細明體"
                        FailureTextStyle-Font-Size="Small"
                        LoginButtonStyle-Font-Names="細明體"
                        LoginButtonStyle-Font-Size="Large"
                        LoginButtonType="Button"
                        TitleText="知識平台"
                        TitleTextStyle-ForeColor="#0071C5"
                        TitleTextStyle-Font-Bold="true"
                        TitleTextStyle-Wrap="true"
                        TitleTextStyle-Font-Names="細明體"
                        TitleTextStyle-Font-Size="34px" TitleTextStyle-Height="40px" LoginButtonStyle-BackColor="#0071C5" LoginButtonStyle-Height="35px" LoginButtonStyle-Width="80px" LoginButtonStyle-ForeColor="White" TextBoxStyle-Height="25px" />
                    
                    <div class="error-message">
                        <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageMessage" Visible="true" />
                        <asp:ValidationSummary id="ValidationSummary1" 
                            runat="server" ValidationGroup="signInControl" >
                        </asp:ValidationSummary>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>