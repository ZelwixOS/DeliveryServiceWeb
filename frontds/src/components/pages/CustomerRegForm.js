import React, { Component } from 'react'
import { Box, Container, Grid, TextField, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import ForwardIcon from '@material-ui/icons/Forward';

const styles = (theme) => ({
    btnMenu: {
        width: '100%',
        textAlign: 'left',
    },
    paper: {
        marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    text: {
        fontSize: '16px',
        padding: theme.spacing(1),
    },
    button: {
        marginTop: theme.spacing(2),
    },

});

class CustomerRegForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            confPassword: "",
            userName: ""
        };

        this.CreateCustomer = this.CreateCustomer.bind(this);
        this.SentCustomer = this.SentCustomer.bind(this);
        this.ErrorNotifier = this.ErrorNotifier.bind(this);


        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
        this.onUserNameChange = this.onUserNameChange.bind(this);
        this.onConfPasswordChange = this.onConfPasswordChange.bind(this);
    }


    onEmailChange(e) { this.setState({ email: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }
    onUserNameChange(e) { this.setState({ userName: e.target.value }); }
    onConfPasswordChange(e) { this.setState({ confPassword: e.target.value }); }

    CreateCustomer(cust) {
        const url = "http://localhost:5000/api/Account/Register";
        var ordJSN = {
            method: 'POST',
            credentials: "same-origin",
            headers: {
                'Accept': 'application/json; charset=UTF-8',
                'Content-Type': 'application/json; charset=UTF-8'
            },
            body: JSON.stringify({
                userName: cust.userName,
                email: cust.email,
                password: cust.password,
                passwordConfirm: cust.passwordConfirm,
            })
        }
        fetch(url, ordJSN)
            .then(response => response.json())
            .then((data) => this.ErrorNotifier(data));
    }

    ErrorNotifier(response) {
        document.querySelector("#response").innerHTML = "";
        var formError = document.querySelector("#formError");
        while (formError.firstChild) {
            formError.removeChild(formError.firstChild);
        }

        document.querySelector("#response").innerHTML = response.message;

        var failed = false;
        if (response.error.length !== undefined)
        {
        if (response.error.length > 0) failed = true;
        if (response.error.length > 0) {
            for (var i = 0; i < response.error.length; i++) {
                let ul = document.querySelector("ul");
                let li = document.createElement("li");
                li.appendChild(document.createTextNode(response.error[i]));
                ul.appendChild(li);
                }
            }
        }
        if (failed)
            return;
        else
            document.location.href = "/";
    }


    SentCustomer(e) {
        e.preventDefault();
        var custEmail = this.state.email.trim();
        var custPassword = this.state.password.trim();
        var custUserName = this.state.userName.trim();
        var custConfPassword = this.state.confPassword.trim();
        if (!custEmail || !custPassword || !custUserName || !custConfPassword) {
            return;
        }
        this.CreateCustomer({ email: custEmail, password: custPassword, passwordConfirm: custConfPassword, userName: custUserName });

    }

    render() {
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />

                <Navbar title={"Регистрация"} />

                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>
                        <Grid>


                            <Card>
                                <CardContent>

                                    <Grid container spacing={3}>
                                        <Grid spacing={3} style={{color: "#F00"}}>
                                            <div id="response" />
                                            <ul id="formError"></ul>
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                id="Login"
                                                required
                                                label="Логин"
                                                fullWidth
                                                onChange={this.onUserNameChange}
                                            />
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                id="email"
                                                required
                                                label="e-mail"
                                                fullWidth
                                                onChange={this.onEmailChange}
                                            />
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                id="password"
                                                required
                                                type="password"
                                                label="Пароль"
                                                fullWidth
                                                onChange={this.onPasswordChange}
                                            />
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                id="confPassword"
                                                required
                                                type="password"
                                                label="Подтвердите пароль"
                                                fullWidth
                                                onChange={this.onConfPasswordChange}
                                            />
                                        </Grid>

                                    </Grid>
                                    <Button
                                        variant="contained"
                                        color="primary"
                                        className={this.props.classes.button}
                                        startIcon={<ForwardIcon />}
                                        onClick={this.SentCustomer}
                                    >
                                        Зарегестрироваться
                                        </Button>

                                </CardContent>
                            </Card>

                        </Grid>
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(CustomerRegForm)

