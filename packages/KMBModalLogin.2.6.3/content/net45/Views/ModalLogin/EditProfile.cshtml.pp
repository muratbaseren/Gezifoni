@model $rootnamespace$.ModalLogin.Models.LoginUser

@{
    ViewBag.Title = "Edit User Profile";
    Layout = "~/Views/Shared/_ModalLoginLayout.cshtml";
}

<style>
    body {
        padding-top: 50px;
    }

    input[type=text],
    input[type=password] {
        width: 280px;
    }

    .input-validation-error {
        border-color: #f00;
    }

    .field-validation-error {
        display: inline-block;
        margin-left: 10px;
        background-color: #f00;
        color: #fff;
        padding: 3px;
        border-radius: 3px;
    }
</style>

<h2>@ViewBag.Title</h2>
<hr />

@using (Html.BeginForm("EditProfile", "ModalLogin", FormMethod.Post, new { enctype = "multipart/form-data" }))
{
    @Html.AntiForgeryToken()
    <div class="form-horizontal">

        <div class="col-md-3">
            @if (string.IsNullOrEmpty(Model.ProfileImageFileName))
            {
                <img src="~/images/user_boy.png" class="img-circle" width="128" height="128">
            }
            else
            {
                <img src="~/images/@Model.ProfileImageFileName" class="img-circle" width="128" height="128" />
            }
            <br /><br />
            <input type="file" name="ProfileImage" id="ProfileImage" class="form-control" /><br />
            <div class="alert alert-info">
                <span class="glyphicon glyphicon-info-sign">&nbsp;</span>
                <span><i>Please use jpg, jpeg or png format.</i></span>
            </div>
        </div>

        <div class="col-md-9">

            @Html.ValidationSummary(true, "", new { @class = "text-danger" })
            @Html.HiddenFor(model => model.Id)

            <div class="form-group">
                @Html.LabelFor(model => model.Name, htmlAttributes: new { @class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.Surname, htmlAttributes: new { @class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(model => model.Surname, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.Surname, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.Username, htmlAttributes: new { @class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(model => model.Username, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.Username, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.Password, htmlAttributes: new { @class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(model => model.Password, new { htmlAttributes = new { @class = "form-control", type = "password" } })
                    @Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.Email, htmlAttributes: new { @class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(model => model.Email, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.Email, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Save" class="btn btn-success" />
                    @Html.ActionLink("Back to Profile", "UserProfile", "ModalLogin", null, new { @class = "btn btn-default" })
                </div>
            </div>

        </div>

    </div>
}
