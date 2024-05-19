<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FamilySignUp.aspx.cs" Inherits="Shahab.CensusRreport.Components.FamilySignUp" %>

<%@ Register Src="~/UserControls/CaptchaControl.ascx" TagPrefix="uc1" TagName="CaptchaControl" %>
<asp:Content ID="cphMiddle" ContentPlaceHolderID="cphMiddle" runat="server">
    <asp:ScriptManager ID="ScriptManagerFamilySignUp" runat="server" EnablePageMethods="true" />
    <asp:Panel ID="pnlMessage" runat="server">
        <ul>
            <asp:Literal ID="liMessage" runat="server" />
        </ul>
    </asp:Panel>
    <fieldset id="fsSignUp" runat="server">
        <legend><span><b>ثبت نام</b></span></legend>
        <div class="mainForm emptyGroup" style="display:inline-block;">
            <div>
                <label id="lblFirstName" class="label reverseAlign topAlign">نام :</label>
                <asp:TextBox ID="txtFirstName" pattern="[^~!@#$%^&*+_=\(\)0-9a-zA-Z]*" MaxLength="250" required placeholder="سعید" runat="server" CssClass="mediumTextBox" />
            </div>
            <div>
                <label id="lblLastName" class="label reverseAlign topAlign">نام خانوادگی :</label>
                <asp:TextBox ID="txtLastName" MaxLength="250" pattern="[^~!@#$%^&*+_=\(\)0-9a-zA-Z]*" required placeholder="سعیدی" runat="server" CssClass="mediumTextBox" />
            </div>
            <div>
                <label id="lblNationalCode" class="label reverseAlign topAlign">شماره ملی :</label>
                <asp:TextBox ID="txtNationalCode" MaxLength="10" required pattern="\d{10}" placeholder="0080921341" runat="server" CssClass="mediumTextBox ltr isNumeric" />
                <asp:HiddenField ID="hdnValidNationalCode" runat="server" />
                <asp:Label ID="lblInValidNationalCode" runat="server" />
            </div>
            <div>
                <label id="lblMobile" class="label reverseAlign">تلفن همراه :</label>
                <asp:TextBox ID="txtMobile" required pattern="09(0[12]|[13][0-9]|2[01])[0-9]{7}" MaxLength="11" placeholder="09122414343" runat="server" CssClass="mediumTextBox ltr isNumeric" />
            </div>
            <div>
                <label id="lblEmail" class="label reverseAlign">پست الکترونیکی :</label>
                <asp:TextBox ID="txtEmail" runat="server" pattern="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" placeholder="test@behdasht.gov.ir" CssClass="mediumTextBox ltr" />
            </div>
            <asp:panel id="pnlOldPassword" runat="server" Visible ="false">
                <label id="lblOldPassword" class="label reverseAlign">رمز ورود قدیمی :</label>
                <asp:TextBox ID="txtOldPassword" pattern="[~!@#$%^&*+_=\(\)0-9a-zA-Z]{6,15}" required MaxLength="15" runat="server" CssClass="mediumTextBox ltr noTransition" TextMode="Password" />
            </asp:panel>
            <div style="margin-bottom:24px;">
                <asp:Label id="lblPassword" class="label reverseAlign" runat="server"/>
                <div class="formToolTip">
                    <asp:TextBox ID="txtPassword" pattern="[~!@#$%^&*+_=\(\)0-9a-zA-Z]{6,15}" required MaxLength="15" runat="server" CssClass="mediumTextBox ltr noTransition" TextMode="Password" />
                    <span class="form_hint">
                        <p>شرایط رمز ورود</p><ul><li>وارد کردن فیلد اجباری است</li><li>باید حداقل شامل شش کاراکتر باشد</li><li>نباید شامل حروف فارسی باشد</li></ul></span>
                </div>
            </div>
            <div>
                <label id="lblConfirmPassword" class="label reverseAlign">تکرار رمز ورود :</label>
                <asp:TextBox ID="txtConfirmPassword" MaxLength="15" required runat="server" CssClass="mediumTextBox ltr noTransition" TextMode="Password" />
            </div>
        </div>
        <div id="dibButtonAndCaptchaWrapper">
            <div class="reverseAlign">
                <uc1:CaptchaControl runat="server" ID="CaptchaControl" />
            </div>
            <div class="reverseAlign">
                <br />
                <asp:Button ID="btnSignup" runat="server" CssClass="btn btn-save" OnClick="btnSignup_Click" />
            </div>
        </div>
    </fieldset>
    <script type="text/javascript">
        $(function () {
            $('#dibButtonAndCaptchaWrapper').css("width", $('.mainForm').width());
            $('.isNumeric').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $('#<%=txtPassword.ClientID%>').passwordChecker();
        });

        $('#<%=txtNationalCode.ClientID%>').change(function () {
            var nationalCode = $(this).val();
            PageMethods.IsValidNationalCode(nationalCode, onSucess, onError);
            function onSucess(result) {
                if (result) {
                    $('#<%=hdnValidNationalCode.ClientID%>').val("1");
                    $('#<%=lblInValidNationalCode.ClientID%>').text('');
                    $('#<%=txtNationalCode.ClientID%>').removeClass("invalid");
                }
                else {
                    $('#<%=hdnValidNationalCode.ClientID%>').val("-1");
                    $('#<%=lblInValidNationalCode.ClientID%>').addClass("red");
                    $('#<%=txtNationalCode.ClientID%>').addClass("invalid");
                    $('#<%=lblInValidNationalCode.ClientID%>').text("شماره ملی نامعتبر است");
                }
            }
            function onError(result) {
                $('#<%=hdnValidNationalCode.ClientID%>').val("-1");
            }

        });

    </script>
</asp:Content>
