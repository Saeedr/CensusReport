<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopLinks.ascx.cs" Inherits="Shahab.CensusReport.UserControls.TopLinks" %>
<asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click" /><span> | </span>
<asp:LinkButton ID="lnkSignup" runat="server" Text="ثبت نام فرد جدید" OnClick="lnkSignup_Click"  /><span> | </span>
<asp:LinkButton ID="lnkRegisterInformation" runat="server" Text="ثبت خانوار" OnClick="lnkRegisterInformation_Click" /><span> | </span>
<asp:LinkButton ID="lnkLogOut" runat="server" Text="خروج" OnClick="lnkLogOut_Click" />