<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FamilyAdd.aspx.cs" Inherits="Shahab.CensusRreport.Components.FamilyAdd" %>

<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>

<asp:Content ID="cphMiddle" ContentPlaceHolderID="cphMiddle" runat="server">
    <asp:ScriptManager ID="ScriptManagerFamilyAdd" runat="server" EnablePageMethods="true" />
    <asp:Panel ID="pnlMessage" runat="server">
        <ul>
            <asp:Literal ID="liMessage" runat="server" />
        </ul>
    </asp:Panel>
    <asp:Panel ID="pnlMainForm" runat="server" Style="margin: 0 auto;">
        <asp:Label ID="lblFamilyId" runat="server" CssClass="hidden" />
        <fieldset id="fsFamily">
            <legend><span><b>اطلاعات خانوار</b></span></legend>
            <div class="mainForm">
                <div class="emptyGroup">

                    <fieldset>
                        <legend><span><b>اطلاعات تماس</b></span></legend>
                        <div>
                            <label id="lblProvince" class="label reverseAlign">نام استان :</label>
                            <asp:DropDownList ID="ddlProvince" runat="server" CssClass="mediumDropdownList isRequired" AppendDataBoundItems="true" onchange="PopulateProvinces();">
                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label id="lblTownShip" class="label reverseAlign">نام شهرستان :</label>
                            <asp:DropDownList ID="ddlTownShip" runat="server" CssClass="mediumDropdownList isRequired" onchange="PopulateTownShips();">
                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label id="lblCity" class="label reverseAlign">نام شهر :</label>
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="mediumDropdownList isRequired">
                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <label id="lblMobileNumber" class="label reverseAlign">تلفن همراه سرپرست :</label>
                            <div class="formToolTip">
                                <asp:TextBox ID="txtMobileNumber" required pattern="09(0[12]|[13][0-9]|2[01])[0-9]{7}" MaxLength="11" placeholder="09122414343" runat="server" CssClass="mediumTextBox ltr isNumeric isRequired" />
                                <span class="form_hint">برای تلفن همراه سرپرست عدد وارد کنید.<br />
                                    09122414343</span>
                            </div>
                        </div>
                        <div>
                            <label id="lblPhoneNumber" class="label reverseAlign">تلفن ثابت خانوار :</label>
                            <div class="formToolTip">
                                <asp:TextBox ID="txtPhoneNumber" required pattern="0[0-9]{5,15}" MaxLength="15" placeholder="02188373555" runat="server" CssClass="mediumTextBox ltr isNumeric isRequired" />
                                <span class="form_hint">تلفن ثابت خود را همراه با کد شهر وارد کنید.<br />
                                    02188373555</span>
                            </div>
                        </div>
                        <div>
                            <label id="lblPostalCode" class="label reverseAlign">کد پستی :</label>
                            <div class="formToolTip">
                                <asp:TextBox ID="txtPostalCode" required pattern="\d{10}" runat="server" MaxLength="10" placeholder="1486777145" CssClass="mediumTextBox ltr isNumeric isRequired" />
                                <span class="form_hint">برای کد پستی عدد وارد کنید.<br />
                                    1486777145</span>
                                <asp:CheckBox ID="chkNoPostalCode" runat="server" Text="کد پستی ندارد" />
                            </div>
                        </div>
                        <div>
                            <label id="lblBuildingNumber" class="label reverseAlign">شماره ساختمان :</label>
                            <div class="formToolTip">
                                <asp:TextBox ID="txtBuildingNumber" required pattern="\d{1,50}" runat="server" MaxLength="50" placeholder="1234" CssClass="mediumTextBox ltr isNumeric isRequired" />
                                <span class="form_hint">برای شماره ساختمان عدد وارد کنید.<br />
                                    1234</span>
                            </div>
                        </div>
                        <div class="topAlign">
                            <label id="lblAddress" class="label reverseAlign topAlign">نشانی خانوار :</label>
                            <div class="formToolTip">
                                <asp:TextBox ID="txtAddress" required runat="server" placeholder="ایران، تهران، شهرک غرب، فاز دوم، خیابان هرمزان، پیروزان جنوبی، کوچه هشتم، پلاک چهار" TextMode="MultiLine" Rows="5" CssClass="mediumTextBox isRequired" />
                                <span class="form_hint">نشانی خود را به طور کامل وارد کنید<br />
                                    مثال: ایران، تهران، شهرک غرب، فاز دوم، خیابان هرمزان، پیروزان جنوبی، کوچه هشتم، پلاک چهار</span>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <div style="visibility: hidden; display: none;">
                        <label id="lblBlockNumber" class="label reverseAlign">شماره بلوک :</label>
                        <div class="formToolTip">
                            <asp:TextBox ID="txtBlockNumber" pattern="\d{1,50}" runat="server" MaxLength="50" placeholder="1234" CssClass="mediumTextBox ltr isNumeric isRequired" />
                            <span class="form_hint">برای شماره بلوک عدد وارد کنید.<br />
                                1234</span>
                        </div>
                    </div>
                    <div style="visibility: hidden; display: none;">
                        <label id="lblFamilyNumber" class="label reverseAlign">شماره خانوار :</label>
                        <div class="formToolTip">
                            <asp:TextBox ID="txtFamilyNumber" pattern="\d{1,50}" runat="server" MaxLength="50" placeholder="1234" CssClass="mediumTextBox ltr isNumeric isRequired" />
                            <span class="form_hint">برای شماره خانوار عدد وارد کنید.<br />
                                1234</span>
                        </div>
                    </div>
                    <div>
                        <label id="lblFamilyType" class="label reverseAlign">نوع خانوار :</label>
                        <div class="formToolTip">
                            <asp:DropDownList ID="ddlFamilyType" runat="server" CssClass="mediumDropdownList isRequired" />
                            <span class="form_hint">نوع خانوار خود را تعیین کنید</span>
                        </div>
                    </div>
                    <div style="visibility: hidden; display: none;">
                        <label id="lblPopulationType" class="label reverseAlign">نوع جمعیت :</label>
                        <asp:DropDownList ID="ddlPopulationType" runat="server" CssClass="mediumDropdownList isRequired" />
                    </div>
                    <div style="visibility: hidden; display: none;">
                        <label id="lblRegionStatus" class="label reverseAlign">وضعیت منطقه شهری :</label>
                        <asp:DropDownList ID="ddlRegionStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                    </div>
                    <div>
                        <label id="lblOwnerShipStatus" class="label reverseAlign">وضعیت مالکیت :</label>
                        <asp:DropDownList ID="ddlOwnerShipStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                    </div>
                </div>
            </div>
        </fieldset>
        <br />
        <asp:Panel ID="pnlMembers" runat="server">
            <div>
                <asp:Button ID="btnNewMember" formnovalidate="formnovalidate" runat="server" CssClass="btn btn-medium correctFloat btn-add" OnClick="btnNewMember_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" Visible="false" formnovalidate="formnovalidate" CssClass="btn btn-delete btn-medium reverseFloat" runat="server" OnClick="btnDelete_Click" />
            </div>
            <br />
            <asp:GridView ID="gvMembers" CssClass="grid" GridLines="None" BorderWidth="0" runat="server" AllowPaging="false" AutoGenerateColumns="false" OnRowDataBound="gvMembers_RowDataBound" OnRowCommand="gvMembers_RowCommand">
                <HeaderStyle CssClass="headerTable" HorizontalAlign="Right" />
                <AlternatingRowStyle CssClass="alternateTable" />
                <RowStyle CssClass="itemTable" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle Width="20" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FirstName" ItemStyle-Width="60"></asp:BoundField>
                    <asp:BoundField DataField="LastName" ItemStyle-Width="100"></asp:BoundField>
                    <asp:BoundField DataField="NationalCode" ItemStyle-Width="80"></asp:BoundField>
                    <asp:TemplateField ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityStatus" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:Label ID="lblRelationShip" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="40">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditMember" Text="ویرایش" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FamilyMemberId" Visible="false"></asp:BoundField>
                </Columns>
            </asp:GridView>
            <fieldset id="fsMember" runat="server" >
                <legend><span><b>اطلاعات اعضای خانوار</b></span></legend>
                <asp:Panel ID="pnlMember" runat="server" style="display: none;">
                    <br />
                    <div class="mainForm">
                        <div>
                            <asp:HiddenField ID="hdnIndex" runat="server" Value="-1" />
                        </div>
                        <fieldset>
                            <legend><span><b>اطلاعات اعضا</b></span></legend>
                            <div>
                                <label id="lblFirstName" class="label reverseAlign topAlign">نام :</label>
                                <div class="formToolTip">
                                    <asp:TextBox ID="txtFirstName" required pattern="[^~!@#$%^&*+_=\(\)0-9a-zA-Z]*" MaxLength="250" placeholder="وحید" runat="server" CssClass="mediumTextBox isRequired" />
                                    <span class="form_hint">نام را وارد کنید.<br />
                                        سعید</span>
                                </div>
                            </div>
                            <div>
                                <label id="lblLastName" class="label reverseAlign topAlign">نام خانوادگی :</label>
                                <div class="formToolTip">
                                    <asp:TextBox ID="txtLastName" MaxLength="250" required pattern="[^~!@#$%^&*+_=\(\)0-9a-zA-Z]*" placeholder="قربانی" runat="server" CssClass="mediumTextBox isRequired" />
                                    <span class="form_hint">نام خانوادگی را وارد کنید.<br />
                                        سعیدی</span>
                                </div>
                            </div>
                            <div>
                                <label id="lblNationality" class="label reverseAlign">ملیت :</label>
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblNationalCode" class="label reverseAlign topAlign">شماره ملی :</label>
                                <div class="formToolTip">
                                    <asp:TextBox ID="txtNationalCode" MaxLength="10" required pattern="\d{10}" placeholder="0080921341" runat="server" CssClass="mediumTextBox ltr isNumeric isRequired" />
                                    <asp:HiddenField ID="hdnValidNationalCode" runat="server" />
                                    <asp:Label ID="lblInValidNationalCode" runat="server" />
                                    <span class="form_hint">برای شماره ملی عدد وارد کنید.<br />
                                        0080942152</span>
                                </div>
                            </div>
                            <div>
                                <label id="lblGender" class="label reverseAlign">جنسیت :</label>
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblRelationShip" class="label reverseAlign">نسبت با سرپرست :</label>
                                <asp:DropDownList ID="ddlRelationShip" runat="server" CssClass="mediumDropdownList isRequired"/>
                            </div>
                            <div>
                                <label id="lblBirthDate" class="label reverseAlign topAlign">تاریخ تولد :</label>
                                <div class="formToolTip">
                                    <asp:TextBox ID="txtBirthDate" MaxLength="10" runat="server" required pattern="((12|13)[0-9]{2})/(0?[1-9]|1[012])/(3[01]|[12][0-9]|0?[1-9])" CssClass="mediumTextBox ltr isDate isRequired" placeholder="1370/05/06" />
                                    <span class="form_hint">تاریخ تولد را به صورت شمسی وارد کنید.<br />
                                        1370/05/06
                                    </span>
                                </div>
                                <asp:Label ID="lblAge" runat="server" />
                                <asp:HiddenField ID="hdnAge" runat="server" />
                            </div>
                        </fieldset>
                        <div class="emptyGroup">
                            <div>
                                <label id="lblInhabitancyStatus" class="label reverseAlign">وضعیت اقامت :</label>
                                <asp:DropDownList ID="ddlInhabitancyStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblMaritalStatus" class="label reverseAlign">وضعیت تاهل :</label>
                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                                <asp:HiddenField id="hdnMaritalStatus" runat="server" />
                            </div>
                            <div>
                                <label id="lblEducationStatus" class="label reverseAlign">وضعیت سواد :</label>
                                <asp:DropDownList ID="ddlEducationStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblActivityStatus" class="label reverseAlign">وضعیت فعالیت :</label>
                                <asp:DropDownList ID="ddlActivityStatus" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblJobType" class="label reverseAlign">نوع شغل :</label>
                                <asp:DropDownList ID="ddlJobType" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblInsuranceFirst" class="label reverseAlign">بیمه پایه اول :</label>
                                <asp:DropDownList ID="ddlInsuranceFirst" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div>
                                <label id="lblInsuranceSecond" class="label reverseAlign">بیمه پایه دوم :</label>
                                <asp:DropDownList ID="ddlInsuranceSecond" runat="server" CssClass="mediumDropdownList isRequired" />
                            </div>
                            <div class="reverseAlign">
                                <asp:Button ID="btnSaveMember" runat="server" CssClass="btn btn-saveMember" OnClick="btnSaveMember_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div style="margin: 5px;">
                    <asp:Button runat="server" ID="btnSave" formnovalidate="formnovalidate" OnClick="btnSave_Click" CssClass="btn btn-save btn-large btn-block" />
                </div>
            </fieldset>
        </asp:Panel>

        <cc1:msgBox ID="MsgBox1" Style="z-index: 103; left: 536px; position: absolute; top: 184px" runat="server"></cc1:msgBox>
    </asp:Panel>

    <script type="text/javascript">

        var pageUrl = '<%=ResolveUrl(Request.RawUrl)%>'
        function PopulateProvinces() {
            $("#<%=ddlTownShip.ClientID%>").attr("disabled", "disabled");
            $("#<%=ddlCity.ClientID%>").attr("disabled", "disabled");
            if ($('#<%=ddlProvince.ClientID%>').val() == "-1") {
                $('#<%=ddlTownShip.ClientID %>').empty().append('<option selected="selected" value="-1">انتخاب کنید</option>');
                $('#<%=ddlCity.ClientID %>').empty().append('<option selected="selected" value="-1">انتخاب کنید</option>');
            }
            else {
                $('#<%=ddlTownShip.ClientID %>').empty().append('<option selected="selected" value="-1">Loading...</option>');
                $.ajax({
                    type: "POST",
                    url: pageUrl + '/PopulatePlaces',
                    data: '{parentId: ' + $('#<%=ddlProvince.ClientID%>').val() + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnTownShipsPopulated,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        }
        function OnTownShipsPopulated(response) {
            PopulateControl(response.d, $("#<%=ddlTownShip.ClientID %>"));
            PopulateTownShips();
        }

        function PopulateTownShips() {
            $("#<%=ddlCity.ClientID%>").attr("disabled", "disabled");

            if ($('#<%=ddlTownShip.ClientID%>').val() == "-1") {
                $('#<%=ddlCity.ClientID %>').empty().append('<option selected="selected" value="-1">انتخاب کنید</option>');
            }
            else {
                $('#<%=ddlCity.ClientID %>').empty().append('<option selected="selected" value="-1">Loading...</option>');
                $.ajax({
                    type: "POST",
                    url: pageUrl + '/PopulatePlaces',
                    data: '{parentId: ' + $('#<%=ddlTownShip.ClientID%>').val() + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnCitiesPopulated,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        function OnCitiesPopulated(response) {
            PopulateControl(response.d, $("#<%=ddlCity.ClientID %>"));
        }

        function PopulateControl(list, control) {
            if (list.length > 0) {
                control.removeAttr("disabled");
                control.empty();
                $.each(list, function () {
                    control.append($("<option></option>").val(this['Value']).html(this['Text']));
                });
            }
            else {
                control.empty().append('<option selected="selected" value="-1">Not available<option>');
            }
        }

        $(function () {

            var indexNationalCode = parseInt(<%= Shahab.CensusRreport.Library.Configurations.IndexNationalCodeColumn %>);;
            var indexRelation = parseInt(<%= Shahab.CensusRreport.Library.Configurations.IndexRelationShipColumn %>);;
            var instituteFamilyTypeValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.InstituteFamilyTypeValue %>);
            var noIranianValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.NoIranianValue %>);
            var noInsuranceValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.NoInsuranceValue %>);
            var supervisorValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.SupervisorValue %>);
            var minAgeConditionValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.MinAgeConditionValue %>);
            var WorkingValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.WorkingValue %>);
            var SpouseRelationValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.SpouseRelationValue %>);
            var HaveSpouseValue = parseInt(<%= Shahab.CensusRreport.Library.Configurations.HaveSpouseValue %>);

            $("#<%=btnNewMember.ClientID%>").click(function (event) {
                $("#<%=pnlMessage.ClientID%>").css("display", "none");
                $("#<%=pnlMember.ClientID%>").css("display", "block");

                if (($("#<%=ddlFamilyType.ClientID%>").val()) == instituteFamilyTypeValue) {
                    $("#<%=ddlRelationShip.ClientID%>")[0].selectedIndex = 0;
                    $("#<%=ddlRelationShip.ClientID%>").prop("disabled", true);
                }
                else {
                    $("#<%=ddlRelationShip.ClientID%>").prop("disabled", false);
                }
                clearFormMember();
                event.preventDefault();
            });

            $('#<%=txtBirthDate.ClientID%>').change(function () {
                var date = $(this).val();
                PageMethods.CalculateAgeFromBirthDate(date, onSucess, onError);
                function onSucess(age) {
                    $('#<%=hdnAge.ClientID%>').val(age);
                    if (age == '' && date != '') {
                        $('#<%=lblAge.ClientID%>').addClass("red");
                        $('#<%=txtBirthDate.ClientID%>').addClass("invalid");
                        $('#<%=lblAge.ClientID%>').text("تاریخ نامعتبر می باشد");
                    }
                    else if (age == '' && date == '') {
                        $('#<%=lblAge.ClientID%>').removeClass("red");
                        $('#<%=txtBirthDate.ClientID%>').removeClass("invalid");
                        $('#<%=lblAge.ClientID%>').text("");
                    }
                    else if (age == '0') {
                        $('#<%=lblAge.ClientID%>').removeClass("red");
                        $('#<%=txtBirthDate.ClientID%>').removeClass("invalid");
                        $('#<%=lblAge.ClientID%>').text("زیر یک سال");
                    }
                    else {
                        $('#<%=lblAge.ClientID%>').removeClass("red");
                        $('#<%=txtBirthDate.ClientID%>').removeClass("invalid");
                        $('#<%=lblAge.ClientID%>').text(age + " سال ");
                    }

            if (parseInt(age) <= minAgeConditionValue) {
                $("#<%=ddlEducationStatus.ClientID%>")[0].selectedIndex = 0;
                $("#<%=ddlActivityStatus.ClientID%>")[0].selectedIndex = 0;
                $("#<%=ddlJobType.ClientID%>")[0].selectedIndex = 0;
                //$("#<%=ddlMaritalStatus.ClientID%>").append('<option selected="selected" value="0">موردی ندارد<option>');
                $("#<%=ddlRelationShip.ClientID%>").val('-1');
                $("#<%=ddlEducationStatus.ClientID%>").prop("disabled", true);
                $("#<%=ddlActivityStatus.ClientID%>").prop("disabled", true);
                $("#<%=ddlJobType.ClientID%>").prop("disabled", true);
                $("#<%=ddlMaritalStatus.ClientID%>").prop("disabled", true);
            }
            else {
                $("#<%=ddlMaritalStatus.ClientID%>")[0].selectedIndex = 0;
                $("#<%=ddlEducationStatus.ClientID%>").prop("disabled", false);
                $("#<%=ddlActivityStatus.ClientID%>").prop("disabled", false);
                $("#<%=ddlJobType.ClientID%>").prop("disabled", false);
                $("#<%=ddlMaritalStatus.ClientID%>").prop("disabled", false);
                //$("#<%=ddlMaritalStatus.ClientID%>" + "option[value='0']").remove();
            }
                }

                function onError(age) {
                    $('#<%=lblAge.ClientID%>').text("");
                }
            });

            $('#<%=ddlActivityStatus.ClientID%>').change(function () {
                var activityValue = $(this).val();
                if (activityValue != WorkingValue && activityValue != -1) {
                    $("#<%=ddlJobType.ClientID%>")[0].selectedIndex = 0;
                    $('#<%=ddlJobType.ClientID%>').prop("disabled", true);
                }
                else {
                    $('#<%=ddlJobType.ClientID%>').prop("disabled", false);
                }
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
                        $('#<%=hdnValidNationalCode.ClientID%>').val("0");
                        $('#<%=lblInValidNationalCode.ClientID%>').addClass("red");
                        $('#<%=txtNationalCode.ClientID%>').addClass("invalid");
                        $('#<%=lblInValidNationalCode.ClientID%>').text("شماره ملی نامعتبر است");
                    }
                }
                function onError(result) {
                    $('#<%=hdnValidNationalCode.ClientID%>').val("0");
                }

            });

            $("#<%=ddlInsuranceFirst.ClientID%>").change(function () {
                if (($(this).val()) == noInsuranceValue) {
                    $("#<%=ddlInsuranceSecond.ClientID%>").val(noInsuranceValue.toString());
                    $("#<%=ddlInsuranceSecond.ClientID%>").prop("disabled", true);
                }
                else {
                    $("#<%=ddlInsuranceSecond.ClientID%>").prop("disabled", false);
                }
            });

            $("#<%=ddlFamilyType.ClientID%>").change(function () {
                if (($(this).val()) == instituteFamilyTypeValue) {
                    $("#<%=ddlRelationShip.ClientID%>")[0].selectedIndex = 0;
                    $("#<%=ddlRelationShip.ClientID%>").prop("disabled", true);
                }
                else {
                    $("#<%=ddlRelationShip.ClientID%>").prop("disabled", false);
                }
            });

            $("#<%=chkNoPostalCode.ClientID%>").change(function () {
                var txtPostalCode = $("#<%=txtPostalCode.ClientID%>");

                if ($(this)[0].checked) {
                    txtPostalCode.removeAttr("placeholder");
                    txtPostalCode.val('');
                    txtPostalCode.removeAttr("required");
                    txtPostalCode.prop("disabled", true);
                }
                else {
                    txtPostalCode.prop("required", true);
                    txtPostalCode.prop("placeholder", "1486777145");
                    txtPostalCode.prop("disabled", false);
                }
            });
            $("#<%=btnSaveMember.ClientID%>").click(function (event) {
                var message = "";
                var newLine = "\r\n"
                var success = true;
                var searchSelfRow = false;
                var rowIndex = 0;
                var hdnIndex = $("#<%=hdnIndex.ClientID%>");

                if ($.trim($(hdnIndex).val()) == "-1") {
                    searchSelfRow = true;
                    rowIndex = 0;
                }
                else {
                    searchSelfRow = false;
                    rowIndex = parseInt($(hdnIndex).val()) + 1;
                }
                var txtNationalCode = $('#<%=txtNationalCode.ClientID %>');
                var ddlNationality = $("#<%=ddlNationality.ClientID%>");
                if (ddlNationality.val() != noIranianValue && $(txtNationalCode).val() != '') {
                    if (!isNumeric($(txtNationalCode).val())) {
                        message += 'برای شماره ملی عدد وارد کنید';
                        message += newLine;
                        success = false;
                    }

                    var search = searchTable('#<%=gvMembers.ClientID%>', $(txtNationalCode).val(), rowIndex, indexNationalCode, searchSelfRow, false);
                    if (search.found) {
                        {
                            message += 'قبلاً این کد ملی را برای یکی از اعضای خانوار ثبت کرده اید.';
                            message += newLine;
                            success = false;
                        }
                    }
                    var hdnValidNationalCode = $('#<%=hdnValidNationalCode.ClientID%>').val();
                    if (hdnValidNationalCode == "0") {
                        message += 'شماره ملی نامعتبر است';
                        message += newLine;
                        success = false;
                    }
                }

                var ddlRelationShip = $('#<%=ddlRelationShip.ClientID %>');
                if (ddlRelationShip.val() == supervisorValue.toString() && !CheckInstituteFamilyType()) {
                    var search = searchTable('#<%=gvMembers.ClientID%>', $(ddlRelationShip).find("option:selected").text(), rowIndex, indexRelation, searchSelfRow, false);
                    if (search.found) {
                        {
                            message = 'قبلاً سرپرست را برای این خانوار ثبت کرده اید.';
                            message += newLine;
                            success = false;
                        }
                    }
                }

                var age = $('#<%=hdnAge.ClientID%>').val();
                var birthDate = $('#<%=txtBirthDate.ClientID%>').val();
                if (age === '' && birthDate != '') {
                    message += 'تاریخ تولد نامعتبر می باشد';
                    message += newLine;
                    success = false;
                }

                if (!CheckInsurance()) {
                    message += 'بیمه پایه اول و دوم نمی تواند هر دو یکسان باشد ';
                    message += newLine;
                    success = false;
                }


                if (!success) {
                    alert(message);
                    event.preventDefault();
                }
            });

            $("#<%=btnSave.ClientID%>").click(function (event) {
                if ($("#<%=txtFirstName.ClientID%>").val() != '' || $("#<%=txtNationalCode.ClientID%>").val() != '' || $("#<%=txtLastName.ClientID%>").val() != '' || $("#<%=txtBirthDate.ClientID%>").val() != '') {
                    $("#<%=btnSave.ClientID%>").removeAttr("formnovalidate");

                }
                else if ($('#<%=gvMembers.ClientID%> tr').length == 0) {
                    $("#<%=btnNewMember.ClientID%>").click();
                    $("#<%=btnSave.ClientID%>").removeAttr("formnovalidate");
                }
            });

            $("#<%=ddlRelationShip.ClientID%>").change(function(){

            });

            $("#<%=ddlNationality.ClientID%>").change(function (event) {

                var txtNationalCode = $("#<%=txtNationalCode.ClientID%>");

                if (($(this).val()) == noIranianValue) {
                    txtNationalCode.val('');
                    txtNationalCode.removeAttr("placeholder");
                    txtNationalCode.prop("disabled", true);
                }
                else {
                    txtNationalCode.prop("placeholder", "0080921341");
                    txtNationalCode.prop("disabled", false);
                }
            });

            $('.isNumeric').keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            function CheckRequiredField(element) {
                var valid = true;
                $(element).find('.isRequired').each(function () {
                    if ($(this).is("input") || $(this).is("textarea")) {
                        if ($(this).val() == '') {
                            $(this).addClass("invalid");
                            valid = false
                        }
                        else {
                            $(this).removeClass("invalid");
                        }
                    }
                    else if ($(this).is("select")) {
                        if ($(this).val() == "-1") {
                            $(this).addClass("invalid");
                            valid = false
                        }
                        else {
                            $(this).removeClass("invalid");
                        }
                    }
                });
                return valid;
            }

            function CheckValidFieldMember(familyFieldCheck) {
                var valid = true;
                if (familyFieldCheck) {
                    valid = CheckValidFieldFamily();
                }
                var patternExpectPersianCahr = /^[^~!@#$%^&*+_=\(\)0-9a-zA-Z]*$/;
                var txtFirstName = $('#<%=txtFirstName.ClientID%>');
                if (txtFirstName.val() != '') {
                    if (!patternExpectPersianCahr.test(txtFirstName.val())) {
                        $(txtFirstName).addClass("invalid");
                        valid = false;
                    }
                    else {
                        $(txtFirstName).removeClass("invalid");
                    }
                }
                var txtLastName = $('#<%=txtLastName.ClientID%>');
                if (txtLastName.val() != '') {
                    if (!patternExpectPersianCahr.test(txtLastName.val())) {
                        $(txtLastName).addClass("invalid");
                        valid = false
                    }
                    else {
                        $(txtLastName).removeClass("invalid");
                    }
                }

                var patternShamsiDate = /^((12|13)[0-9]{2})\/(0?[1-9]|1[012])\/(3[01]|[12][0-9]|0?[1-9])$/;

                var txtBirthDate = $('#<%=txtBirthDate.ClientID%>');
                var age = $('#<%=hdnAge.ClientID%>').val();
                if (txtBirthDate.val() != '') {
                    if (!patternShamsiDate.test(txtBirthDate.val()) || age === '') {
                        $(txtBirthDate).addClass("invalid");
                        valid = false
                    }
                    else {
                        $(txtBirthDate).removeClass("invalid");
                    }
                }

                var validNationalCode = $('#<%=hdnValidNationalCode.ClientID%>').val();
                if (validNationalCode != '') {
                    if (validNationalCode == '0') {
                        $('#<%=txtNationalCode.ClientID%>').addClass("invalid");
                        valid = false
                    }
                    else {
                        $('#<%=txtNationalCode.ClientID%>').removeClass("invalid");
                    }
                }

                return valid;
            }

            function CheckValidFieldFamily() {
                var valid = true;
                $('#fsFamily').find('.isNumeric').each(function () {
                    if ($(this).val() != '') {
                        if (!isNumeric($(this).val())) {
                            $(this).addClass("invalid");
                            valid = false;
                        }
                        else {
                            $(this).removeClass("invalid");
                        }
                    }
                });

                var txtPostalCode = $('#<%=txtPostalCode.ClientID%>');
                if (txtPostalCode.val() != '') {
                    if (txtPostalCode.val().length != 10) {
                        $(txtPostalCode).addClass("invalid");
                        valid = false
                    }
                    else {
                        $(txtPostalCode).removeClass("invalid");
                    }
                }

                var patternMobile = /^09(0[12]|[13][0-9]|2[01])[0-9]{7}$/;

                var txtMobileNumber = $('#<%=txtMobileNumber.ClientID%>');
                if (txtMobileNumber.val() != '') {
                    if (!patternMobile.test(txtMobileNumber.val()) || txtMobileNumber.val().length != 11) {
                        $(txtMobileNumber).addClass("invalid");
                        valid = false
                    }
                    else {
                        $(txtMobileNumber).removeClass("invalid");
                    }
                }

                var patternPhone = /^0[0-9]{5,15}$/;

                var txtPhoneNumber = $('#<%=txtPhoneNumber.ClientID%>');
                if (txtPhoneNumber.val() != '') {
                    if (!patternPhone.test(txtPhoneNumber.val()) || txtPhoneNumber.val().length < 5 || txtPhoneNumber.val().length > 15) {
                        $(txtPhoneNumber).addClass("invalid");
                        valid = false
                    }
                    else {
                        $(txtPhoneNumber).removeClass("invalid");
                    }
                }

                return valid;
            }

            $('.isDate').keypress(function (e) {
                var birthdate = $(this).val();

                if (e.which != 8 && e.which != 0 && (e.which < 47 || e.which > 57)) {
                    return false;
                }

                if (e.which != 47 && (birthdate.length == 4 || birthdate.length == 7)) {
                    var temp = $(this).val().concat("/");
                    $(this).val(temp);
                }
                else if (e.which == 47 && (birthdate.length != 4 && birthdate.length != 7)) {
                    {
                        return false;
                    }
                }
            });

            function isNumeric(input) {
                var pattern = /^\d*$/;
                return pattern.test(input);
            }

            function CheckInstituteFamilyType() {
                var ddlFamilyType = $('#<%=ddlFamilyType.ClientID%>');
                if (ddlFamilyType.val() == instituteFamilyTypeValue.toString())
                    return true;
                else
                    return false;
            }

            function CheckInsurance() {
                var ddlInsuranceFirst = $('#<%=ddlInsuranceFirst.ClientID%>');
                var ddlInsuranceSecond = $('#<%=ddlInsuranceSecond.ClientID%>');

                if (ddlInsuranceFirst.val() == ddlInsuranceSecond.val() && ddlInsuranceFirst.val() != "-1" && ddlInsuranceFirst.val() != noInsuranceValue)
                    return false;
                else
                    return true;
            }

            function clearFormMember() {
                $('#<%=hdnIndex.ClientID%>').val('-1');
                $('#<%=hdnAge.ClientID%>').val('');
                $('#<%=lblAge.ClientID%>').text('');
                $('#<%=txtFirstName.ClientID%>').val('');
                $('#<%=txtLastName.ClientID%>').val('');
                $('#<%=ddlNationality.ClientID%>')[0].selectedIndex = 0;
                $('#<%=txtNationalCode.ClientID%>').val('');
                $('#<%=ddlGender.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlRelationShip.ClientID%>')[0].selectedIndex = 0;
                $('#<%=txtBirthDate.ClientID%>').val('');
                $('#<%=ddlInhabitancyStatus.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlMaritalStatus.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlEducationStatus.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlActivityStatus.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlJobType.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlInsuranceFirst.ClientID%>')[0].selectedIndex = 0;
                $('#<%=ddlInsuranceSecond.ClientID%>')[0].selectedIndex = 0;
                $('#<%=txtNationalCode.ClientID%>').prop("disabled", false);
                $('#<%=ddlEducationStatus.ClientID%>').prop("disabled", false);
                $('#<%=ddlActivityStatus.ClientID%>').prop("disabled", false);
                $('#<%=ddlJobType.ClientID%>').prop("disabled", false);
                $('#<%=ddlInsuranceSecond.ClientID%>').prop("disabled", false);
                $('#<%=ddlMaritalStatus.ClientID%>').prop("disabled", false);
            }

            function searchTable(tableId, inputVal, rowNumber, columnNumber, searchSelfRow, reverseInputVal) {
                var found = false;
                var count = 0;
                if (inputVal.trim()) {
                    $(tableId).find('tr').each(function (indexRow, row) {
                        if ((searchSelfRow && (rowNumber == 0 || rowNumber == indexRow))
                            || (!searchSelfRow && rowNumber != indexRow)) {

                            var allCells = $(row).find('td');
                            if (allCells.length > 0) {
                                allCells.each(function (indexColumn, td) {
                                    if (columnNumber == 0 || columnNumber == indexColumn) {
                                        if (!reverseInputVal) {
                                            if ($.trim($(td).text()) === $.trim(inputVal)) {
                                                found = true;
                                                count++;
                                                return false;
                                            }
                                        }
                                        else {
                                            if ($.trim($(td).text()) != $.trim(inputVal)) {
                                                found = true;
                                                count++;
                                                return false;
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    });
                }
                return { found: found, count: count };
            }
        });
    </script>

</asp:Content>
