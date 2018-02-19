@using $rootnamespace$.ModalLogin.Models
@model LoginUser
@{
    ViewBag.Title = "User Profile";
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
</style>

<h2>@ViewBag.Title</h2>
<hr />

<br><br>
<div class="container">
    <div class="row">
        <div class="col-md-2">
            @if (string.IsNullOrEmpty(Model.ProfileImageFileName))
            {
                <img src="~/images/user_boy.png" class="img-circle" width="128" height="128">
            }
            else
            {
                <img src="~/images/@Model.ProfileImageFileName" class="img-circle" width="128" height="128" />
            }
        </div>

        <div class="col-md-8">
            <h3>@(Model.Name + " " + Model.Surname)</h3>
            <br />
            <h6><b>Username: </b>@Model.Username</h6>
            <h6><b>Email: </b>@Model.Email</h6>
            <br />
            <h6><a href="#">More... </a></h6>
        </div>

        <div class="col-md-2 text-right">
            <div class="btn-group">
                <a class="btn dropdown-toggle btn-info" data-toggle="dropdown" href="#">
                    <span class="glyphicon glyphicon-cog">&nbsp;</span>
                    Options
                    <span class="icon-cog icon-white"></span><span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                    <li><a href="@Url.Action("EditProfile")"><span class="glyphicon glyphicon-edit">&nbsp;</span> Modify</a></li>
                    <li><a href="@Url.Action("DeleteProfile")" onclick="return confirm('Are you sure remove this account?');"><span class="glyphicon glyphicon-trash">&nbsp;</span> Delete</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
