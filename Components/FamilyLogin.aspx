<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FamilyLogin.aspx.cs" Inherits="Shahab.CensusReport.Components.FamilyLogin" %>
<asp:Content ID="cphMiddle" ContentPlaceHolderID="cphMiddle" runat="server">
    <div>
    <asp:Panel ID="pnlMessage" runat="server">
        <ul>
            <asp:Literal ID="liMessage" runat="server" />
        </ul>
    </asp:Panel>
     <fieldset>
        <legend><span><b>ورود به سیستم</b></span></legend>
         <div class="mainForm emptyGroup" style="display: inline-block;">
             <div>
                 <label id="lblNationalCode" class="label reverseAlign">شماره ملی :</label>
                <asp:TextBox ID="txtNationalCode" MaxLength="10" required pattern="\d{10}" runat="server" autocomplete="off" CssClass="mediumTextBox ltr isNumeric noTransition" />
            </div>
             <div>
                 <label id="lblPassword" class="label reverseAlign">رمز ورود :</label>
                <asp:TextBox ID="txtPassword" MaxLength="15" pattern="[~!@#$%^&*+_=\(\)0-9a-zA-Z]+" required runat="server" CssClass="mediumTextBox ltr noTransition" TextMode="Password" />
            </div>
             <div class="reverseAlign">
                <br />
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-save" OnClick="btnLogin_Click"/>
            </div>
         </div>
         </fieldset>
        </div>
        <script type="text/javascript">
            $(function () {
            $('#<%=txtPassword.ClientID%>').passwordChecker();
            $('.isNumeric').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });

    </script>
</asp:Content>
