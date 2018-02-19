@model $rootnamespace$.ModalLogin.ResetPasswordViewModel

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.Password, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-4">
                @Html.EditorFor(model => model.Password, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.PasswordRepeat, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-4">
                @Html.EditorFor(model => model.PasswordRepeat, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.PasswordRepeat, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <button type="submit" value="Send" class="btn btn-success">
                    <span class="glyphicon glyphicon-send">&nbsp;</span> Send
                </button>
            </div>
        </div>
    </div>
}
