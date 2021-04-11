import React, { Component } from 'react'
import { Grid,  Button, TextField, Link } from '@material-ui/core'
import NavigateNextRoundedIcon from '@material-ui/icons/NavigateNextRounded';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import ForwardIcon from '@material-ui/icons/Forward';
import Divider from '@material-ui/core/Divider';

class LogInForm extends Component {


    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            open: false
        };

        this.SentItem = this.SentItem.bind(this);
        this.Authorize = this.Authorize.bind(this);


        this.onEmailChange = this.onEmailChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);

        this.handleClickOpen = this.handleClickOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
    }


    onEmailChange(e) { this.setState({ email: e.target.value }); }
    onPasswordChange(e) { this.setState({ password: e.target.value }); }

    handleClickOpen()  {
        this.setState({open: true})
    }

    handleClose(){
        this.setState({open: false})
    }

    Authorize(userInfo) {
        const url = "https://localhost:5001/api/OrderItems/";// no idea yet
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                email: userInfo.orderName,
                password: userInfo.password
            })
        }
        fetch(url, ordJSN);
        // редирект 
    }

    SentItem(e) {
        e.preventDefault();
        var userEmail = this.state.email.trim();
        var userPassword = this.state.email.trim();


        if (!userEmail || !userPassword) {
            return;
        }
        this.Authorize({ email: userEmail, password: userPassword });
    }

    render() {
        return (
            <React.Fragment>
               <Button variant="contained" startIcon={<ForwardIcon />} onClick={this.handleClickOpen}>
					Войти
				</Button>
                <Dialog open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">   {/* передай handleClose и open */}
                    <DialogTitle id="form-dialog-title">Вход</DialogTitle>
                    <Divider />
                    <DialogContent>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="Адрес эл. почты"
                                fullWidth
                                onChange={this.onEmailChange}
                            />
                        </Grid>

                        <Grid item xs={12}>
                            <TextField
                                required
                                label="Пароль"
                                fullWidth
                                onChange={this.onPasswordChange}
                            />
                        </Grid>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleClose} color="primary">
                            Закрыть
                        </Button>
                        <Button onClick={this.SentItem} color="primary">
                             Войти <NavigateNextRoundedIcon style={{ fontSize: 35, color: "#3B14AF" }} />
                        </Button>
                    </DialogActions>
                    <Divider />
                    <Link href="/registration" style={{textAlign: "center"}}>Ещё не зарегистрированы?</Link>
                </Dialog>
            </React.Fragment>
        )
    }
}
export default (LogInForm)

