<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="CaptchaControl.ascx.cs" Inherits="Shahab.CensusRreport.UserControls.CaptchaControl" %>
<div style="display: inline-block;">
    <div>
        <asp:Image ID="imgCaptcha" runat="server" />
        <asp:LinkButton ID="lblSpeak" runat="server" OnClick="lblSpeak_Click" Width="40"><img src="/Image/speech.png"/></asp:LinkButton>
    </div>
    <div>
        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="captchaTextBox ltr"></asp:TextBox>
        <span style="width: 40px; text-align: center; display: inline-block;">
            <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click">
        <img src="/Image/refresh.png" />
            </asp:LinkButton>
        </span>
    </div>
    <div style="text-align: right;">
        <asp:Label ID="lblResult" runat="server" CssClass="red" /></div>
</div>
