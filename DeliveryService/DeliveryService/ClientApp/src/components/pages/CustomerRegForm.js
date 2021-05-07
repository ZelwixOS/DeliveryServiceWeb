import React, { Component } from 'react'
import { Box, Container, Grid, TextField, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import ForwardIcon from '@material-ui/icons/Forward';
import axios from 'axios';


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
const comUrl = "http://localhost:5000" 

class CourierRegForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            confPassword: "",
            userName: "",
            firstName: "",
            secondName: ""
        };

        this.CreateCourier = this.CreateCourier.bind(this);
        this.SentCourier = this.SentCourier.bind(this);
        this.ErrorNotifier = this.ErrorNotifier.bind(this);


        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
        this.onUserNameChange = this.onUserNameChange.bind(this);
        this.onConfPasswordChange = this.onConfPasswordChange.bind(this);
        this.onFirstNameChange = this.onFirstNameChange.bind(this);
        this.onSecondNameChange = this.onSecondNameChange.bind(this);
    }


    onEmailChange(e) { this.setState({ email: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }
    onUserNameChange(e) { this.setState({ userName: e.target.value }); }
    onConfPasswordChange(e) { this.setState({ confPassword: e.target.value }); }
    onFirstNameChange(e) { this.setState({ firstName: e.target.value }); }
    onSecondNameChange(e) { this.setState({ secondName: e.target.value }); }

    CreateCourier(cour) {
        const url = comUrl + "/api/Account/Register";
            var value = { 
                "userName": cour.userName,
                "email": cour.email,
                "password":  cour.password,
                "passwordConfirm": cour.passwordConfirm,
                "firstName": cour.firstName

                };
               axios.post(url, value).then((data) => this.ErrorNotifier(data.data));
    }

    ErrorNotifier(response) {
        document.querySelector("#response").innerHTML = "";
        var formError = document.querySelector("#formError");
        while (formError.firstChild) {
            formError.removeChild(formError.firstChild);
        }

        document.querySelector("#response").innerHTML = response.message;

        var failed = false;
        if (response.error !== undefined)
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


    SentCourier(e) {
        e.preventDefault();
        var courEmail = this.state.email.trim();
        var courPassword = this.state.password.trim();
        var courUserName = this.state.userName.trim();
        var courConfPassword = this.state.confPassword.trim();
        var courFirstName = this.state.firstName.trim();
        var courSecondName = this.state.secondName.trim();
        if (!courEmail || !courPassword || !courUserName || !courConfPassword || !courFirstName || !courSecondName) {
            return;
        }
        this.CreateCourier({ email: courEmail, password: courPassword, passwordConfirm: courConfPassword, userName: courUserName, firstName: courFirstName, secondName: courSecondName });

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
                                                id="FirstName"
                                                required
                                                label="Имя"
                                                fullWidth
                                                onChange={this.onFirstNameChange}
                                            />
                                        </Grid>

                                        <Grid item xs={12}>
                                            <TextField
                                                id="SecondName"
                                                required
                                                label="Фамилия"
                                                fullWidth
                                                onChange={this.onSecondNameChange}
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
                                        onClick={this.SentCourier}
                                    >
                                        Зарегистрироваться
                                        
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
export default withStyles(styles)(CourierRegForm)

