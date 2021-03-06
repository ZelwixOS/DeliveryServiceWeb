import React, { Component } from 'react'
import { Grid, Button, TextField, Link } from '@material-ui/core'
import NavigateNextRoundedIcon from '@material-ui/icons/NavigateNextRounded';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import ForwardIcon from '@material-ui/icons/Forward';
import Divider from '@material-ui/core/Divider';
import axios from 'axios';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';

axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000" 


class LogInForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            userName: "",
            password: "",
            rememberMe: true,
            open: false,
            isLogin: false,
            login: "",
            error: null
        };

        this.SentItem = this.SentItem.bind(this);
        this.Authorize = this.Authorize.bind(this);
        this.ErrorNotifier = this.ErrorNotifier.bind(this);
        this.LogOut = this.LogOut.bind(this);
        this.getCurrentUser = this.getCurrentUser.bind(this);

        this.onUserNameChange = this.onUserNameChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);

        this.handleClickOpen = this.handleClickOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }


    onUserNameChange(e) { this.setState({ userName: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }

    handleClickOpen() { this.setState({ open: true }) }
    handleClose() { this.setState({ open: false }) }
    handleChange(e) { this.setState({ rememberMe: e.target.checked }) }


    LogOut() {
        const url =  comUrl + "/api/Account/LogOut/";
        axios.post(
            url, { withCredentials: true }
        ).then( document.location.href = "/");
    };

    Authorize(userInfo) {

        const url = comUrl +  "/api/Account/LogIn/";
        var value = { 
         "userName": userInfo.userName,
         "password": userInfo.password,
         "rememberMe": userInfo.rememberMe
         };
        axios.post(
            url, value, { withCredentials: true }
        ).then((response) => this.setState({ error: response.data.error})).then(this.ErrorNotifier);
    }

    ErrorNotifier() {
        var failed = false;
        if (this.state.error !== undefined && this.state.error !== null) 
            if (this.state.error.length > 0) failed = true;
   
        if (failed)
            return;
        else
            document.location.href = "/";
    }


    SentItem(e) {
        e.preventDefault();
        var userUserName = this.state.userName.trim();
        var userPassword = this.state.password.trim();


        if (!userUserName || !userPassword) {
            return;
        }
        this.Authorize({ userName: userUserName, password: userPassword, rememberMe: this.state.rememberMe });
    }

    getCurrentUser() {
        const url = comUrl + "/api/Account/isAuthenticated/";
        axios.post(
            url, { withCredentials: true }
        ).then(result => {
                    if (result.data.message.length > 0) {
                        this.setState({ login: result.data.message, isLogin: true })
                    }
                });
    }


    componentDidMount() {
        this.getCurrentUser();
    }

    render() {
        return (
            <React.Fragment>
                {
                    this.state.isLogin
                        ?
                        <React.Fragment >
                            
                            <Button onClick={this.LogOut} variant="contained" color="secondary"  startIcon={<ExitToAppIcon />}>
                                {this.state.login}
                            </Button>
                        </React.Fragment>
                        :

                        <Button  variant="contained" startIcon={<ForwardIcon />} onClick={this.handleClickOpen}>
                            ??????????
				        </Button>
                }

                <Dialog open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">   {/* ?????????????? handleClose ?? open */}
                    <DialogTitle id="form-dialog-title">????????</DialogTitle>
                    <Divider />
                    <DialogContent>
                        <Grid container spacing={3} style={{ color: "#F00" }}>
                            {
                                this.state.error !== undefined && this.state.error !== null && this.state.error.length > 0  && 
                                <ul> 
                                    ????????????:
                                    {
                                        this.state.error.map((er, index) => { return (<li key={index}>{er}</li>) })
                                    }
                                </ul>

                            }
                            <div id="response" />
                            <ul id="formError"></ul>
                        </Grid>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="??????????"
                                fullWidth
                                onChange={this.onUserNameChange}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="????????????"
                                type="password"
                                fullWidth
                                onChange={this.onPasswordChange}
                            />
                        </Grid>
                        <FormControlLabel control={<Checkbox checked={this.state.rememberMe} onChange={this.handleChange} name="checkedA" />} label="?????????????????? ????????" />

                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose} color="primary">
                            ??????????????
                        </Button>
                        <Button onClick={this.SentItem} color="primary">
                            ?????????? <NavigateNextRoundedIcon style={{ fontSize: 35, color: "#3B14AF" }} />
                        </Button>
                    </DialogActions>
                    <Divider />
                    <Link href="/registration" style={{ textAlign: "center" }}>?????? ???? ?????????????????????????????????</Link>
                </Dialog>
            </React.Fragment>
        )
    }
}
export default (LogInForm)

