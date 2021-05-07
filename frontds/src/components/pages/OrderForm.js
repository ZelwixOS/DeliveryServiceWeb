import React, { Component } from 'react'
import { Box, Container, Grid, Typography, TextField, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import SaveIcon from '@material-ui/icons/Save';
import axios from 'axios';


axios.defaults.withCredentials = true
const comUrl = "http://localhost:5000" 


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

class OrderForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            order: null,
            loading: true,
            adressOrigin: "",
            deadline: new Date(Date.now()).toISOString().split('T')[0],
            adressDestination: "",
            receiverName: "",
            addNote: ""
        };

        this.CreateOrder = this.CreateOrder.bind(this);
        this.EditOrder = this.EditOrder.bind(this);
        this.SentOrder = this.SentOrder.bind(this);



        this.onAdressOriginChange = this.onAdressOriginChange.bind(this);
        this.onDeadlineChange = this.onDeadlineChange.bind(this);
        this.onAdressDestinationChange = this.onAdressDestinationChange.bind(this);
        this.onReceiverNameChange = this.onReceiverNameChange.bind(this);
        this.onAddNoteChange = this.onAddNoteChange.bind(this);

    }

    onAdressOriginChange(e) { this.setState({ adressOrigin: e.target.value }); }
    onDeadlineChange(e) { this.setState({ deadline: e.target.value }); }
    onAdressDestinationChange(e) { this.setState({ adressDestination: e.target.value }); }
    onReceiverNameChange(e) { this.setState({ receiverName: e.target.value }); }
    onAddNoteChange(e) { this.setState({ addNote: e.target.value }); }


    componentDidMount() {
        if (this.props.orderID !== undefined)
        {
            const url = comUrl+ "/api/orders/" + this.props.orderID;
            axios.get(url).then(result =>
                this.setState({
                    order: result.data,
                    loading: false,
                    adressOrigin: result.data.adressOrigin,
                    deadline: result.data.deadline,
                    adressDestination: result.data.adressDestination,
                    receiverName: result.data.receiverName,
                    addNote: result.data.addNote}));
        }
        else
        this.setState({loading: false});

    }

    CreateOrder(orderO) {
        const url = comUrl+ "/api/NewOrder";
        var value = { 
            "adressOrigin": orderO.adressOrigin,
            "deadline": orderO.deadline,
            "adressDestination": orderO.adressDestination,
            "receiverName": orderO.receiverName,
            "addNote": orderO.addNote
            };
           axios.post(url, value);
    }

    EditOrder(orderO) {
        const url = comUrl+ "/api/PutOrder/" + orderO.id;

        var value = { 
            "adressOrigin": orderO.adressOrigin,
            "deadline": orderO.deadline,
            "adressDestination": orderO.adressDestination,
            "receiverName": orderO.receiverName,
            "addNote": orderO.addNote
            };
            
           axios.put(url, value);
    }

    SentOrder(e) {
        e.preventDefault();
        var orderAdressOrigin = this.state.adressOrigin.trim();
        var orderDeadline;
        if (this.state.deadline!== undefined)
        orderDeadline = this.state.deadline;
        else
        orderDeadline = new Date(Date.now()).toISOString().split('T')[0];

        var orderAdressDestination = this.state.adressDestination.trim();
        var orderReceiverName = this.state.receiverName.trim();
        var orderAddNote;
        if (this.state.addNote !== null && this.state.addNote!== undefined)
            orderAddNote = this.state.addNote.trim();
        else
            orderAddNote = null;

        if (!orderAdressOrigin || !orderDeadline || !orderAdressDestination || !orderReceiverName ) {
            return;
        }
        var formDeadline = new Date(orderDeadline);
        if (this.props.orderID === undefined)
            this.CreateOrder({ adressOrigin: orderAdressOrigin, deadline: formDeadline, adressDestination: orderAdressDestination, receiverName: orderReceiverName, addNote: orderAddNote});
        else
            this.EditOrder({ id: this.props.orderID, adressOrigin: orderAdressOrigin, deadline: formDeadline, adressDestination: orderAdressDestination, receiverName: orderReceiverName, addNote: orderAddNote});
        // document.location.href = "/";
    }

    render() {
        
        var deadline, adressOrigin, adressDestination, receiverName, addNote;
        if (this.props.orderID === undefined || this.state.order === null) {
            deadline = new Date(Date.now()).toISOString().split('T')[0]; adressOrigin = ""; adressDestination = ""; receiverName = ""; addNote = ""; 
        }
        else {
            deadline = this.state.order.deadline.toString().substring(0, 10);
            adressOrigin = this.state.order.adressOrigin;
            adressDestination = this.state.order.adressDestination;
            receiverName = this.state.order.receiverName;
            addNote = this.state.order.addNote;


        }
        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                {
                    this.state.loading || this.props.orderID === undefined
                        ?
                        <Navbar title={"Заказ"} />
                        :
                        <Navbar title={"Заказ " + this.state.order.id} />
                }

                <Box className={this.props.classes.paper}>
                    <Grid container spacing={1}>
                        <Grid>
                            {
                                this.state.loading
                                    ? <Typography className={this.props.classes.loading}>
                                        Загрузка...
                                    </Typography >
                                    :

                                    <Card>

                                        <CardContent>
                                      
                                            
                                                <Grid container spacing={3}>

                                                    <Grid item xs={12} sm={6}>
                                                    <TextField
                                                        type="date"
                                                        id="deadline"
                                                        required
                                                        label="Крайний срок"
                                                        fullWidth
                                                        defaultValue={deadline}
                                                        onChange={this.onDeadlineChange}
                                                    />
                                                </Grid>

                                                    <Grid item xs={12}>
                                                        <TextField
                                                            id="adressOrigin"
                                                            required
                                                            label="Адрес отправления"
                                                            fullWidth
                                                            defaultValue={adressOrigin}
                                                            onChange={this.onAdressOriginChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12}>
                                                        <TextField
                                                            id="adressDestination"
                                                            required
                                                            label="Адрес доставки"
                                                            fullWidth
                                                            defaultValue={adressDestination}
                                                            onChange={this.onAdressDestinationChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12}>
                                                        <TextField
                                                            id="receiverName"
                                                            required
                                                            label="Получатель"
                                                            fullWidth
                                                            defaultValue={receiverName}
                                                            onChange={this.onReceiverNameChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12}>
                                                        <TextField
                                                            id="addNote"
                                                            label="Примечание"
                                                            fullWidth
                                                            defaultValue={addNote}
                                                            onChange={this.onAddNoteChange}
                                                        />
                                                    </Grid>


                                                </Grid>
                                                <Button
                                                    variant="contained"
                                                    color="primary"
                                                    className={this.props.classes.button}
                                                    startIcon={<SaveIcon />}
                                                    onClick={this.SentOrder}
                                                >
                                                    Сохранить
                                        </Button>
                                         
                                        </CardContent>
                                    </Card>
                            }
                        </Grid>
                    </Grid>
                </Box>
            </Container >
        )
    }
}
export default withStyles(styles)(OrderForm)

