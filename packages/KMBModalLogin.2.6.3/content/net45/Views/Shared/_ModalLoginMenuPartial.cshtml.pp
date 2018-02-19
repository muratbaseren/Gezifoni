@using $rootnamespace$.ModalLogin.Models

<ul class="nav navbar-nav navbar-right">
    @if (Session["login"] != null)
    {
        var username = (Session["login"] as LoginUser).Username;

        <li>
            <a href="/ModalLogin/UserProfile">
                <span class="glyphicon glyphicon-user">&nbsp;</span>@username
            </a>
        </li>
        <li>
            <a href="/ModalLogin/SignOut">
                <span class="glyphicon glyphicon-log-out">&nbsp;</span>Logout
            </a>
        </li>
    }
    else
    {
        <li>
            <a href="#" role="button" data-toggle="modal" data-target="#login-modal" data-openmode="login">
                <span class="glyphicon glyphicon-log-in">&nbsp;</span>Sign In
            </a>
        </li>
        <li>
            <a href="#" role="button" data-toggle="modal" data-target="#login-modal" data-openmode="register">
                <span class="glyphicon glyphicon-user">&nbsp;</span>Sign Up
            </a>
        </li>
    }
</ul>