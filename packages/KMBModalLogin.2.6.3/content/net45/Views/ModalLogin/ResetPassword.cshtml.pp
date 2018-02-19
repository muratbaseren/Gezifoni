@model $rootnamespace$.ModalLogin.ResetPasswordViewModel

@{
    ViewBag.Title = "Reset Password";
    Layout = "~/Views/Shared/_ModalLoginLayout.cshtml";
}

<h2>Reset Password</h2>
<hr />

<div class="row">
    @Html.Partial("_ModalLoginResetPasswordPartial", Model)
</div>


<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>