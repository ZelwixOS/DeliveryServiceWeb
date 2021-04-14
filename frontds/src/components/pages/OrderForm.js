import React, { Component } from 'react'
import { Box, Container, Grid, Typography, TextField, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import { withStyles } from "@material-ui/core/styles"
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import SaveIcon from '@material-ui/icons/Save';

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
            addNote: "",
            delivery_ID_FK: "",
            customer_ID_FK: "",
            status_ID_FK: "",
            courier_ID_FK: ""
        };

        this.CreateOrder = this.CreateOrder.bind(this);
        this.EditOrder = this.EditOrder.bind(this);
        this.SentOrder = this.SentOrder.bind(this);



        this.onAdressOriginChange = this.onAdressOriginChange.bind(this);
        this.onDeadlineChange = this.onDeadlineChange.bind(this);
        this.onAdressDestinationChange = this.onAdressDestinationChange.bind(this);
        this.onReceiverNameChange = this.onReceiverNameChange.bind(this);
        this.onAddNoteChange = this.onAddNoteChange.bind(this);
        this.onDelivery_ID_FKChange = this.onDelivery_ID_FKChange.bind(this);
        this.onCustomer_ID_FKChange = this.onCustomer_ID_FKChange.bind(this);
        this.onStatus_ID_FKChange = this.onStatus_ID_FKChange.bind(this);
        this.onCourier_ID_FKChange = this.onCourier_ID_FKChange.bind(this);
    }

    onAdressOriginChange(e) { this.setState({ adressOrigin: e.target.value }); }
    onDeadlineChange(e) { this.setState({ deadline: e.target.value }); }
    onAdressDestinationChange(e) { this.setState({ adressDestination: e.target.value }); }
    onReceiverNameChange(e) { this.setState({ receiverName: e.target.value }); }
    onAddNoteChange(e) { this.setState({ addNote: e.target.value }); }
    onDelivery_ID_FKChange(e) { this.setState({ delivery_ID_FK: e.target.value }); }
    onCustomer_ID_FKChange(e) { this.setState({ customer_ID_FK: e.target.value }); }
    onStatus_ID_FKChange(e) { this.setState({ status_ID_FK: e.target.value }); }
    onCourier_ID_FKChange(e) { this.setState({ courier_ID_FK: e.target.value }); }


    componentDidMount() {
        const url = "http://localhost:5000/api/orders/" + this.props.orderID;
            fetch(url, { method: 'GET' })
                .then(response => response.json())
                .then(result =>
                    this.setState({
                        order: result,
                        loading: false,
                        adressOrigin: result.adressOrigin,
                        deadline: result.deadline,
                        adressDestination: result.adressDestination,
                        receiverName: result.receiverName,
                        addNote: result.addNote,
                        delivery_ID_FK: result.delivery_ID_FK,
                        customer_ID_FK: result.customer_ID_FK,
                        status_ID_FK: result.status_ID_FK,
                        courier_ID_FK: result.courier_ID_FK
                    }));
    }



    CreateOrder(orderO) {
        const url = "http://localhost:5000/api/orders/";
        var ordJSN = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                adressOrigin: orderO.adressOrigin,
                deadline: orderO.deadline,
                adressDestination: orderO.adressDestination,
                receiverName: orderO.receiverName,
                addNote: orderO.addNote,
                delivery_ID_FK: orderO.delivery_ID_FK,
                customer_ID_FK: orderO.customer_ID_FK,
                status_ID_FK: orderO.status_ID_FK,
                courier_ID_FK: orderO.courier_ID_FK
            })
        }
        fetch(url, ordJSN);
    }

    EditOrder(orderO) {
        const url = "http://localhost:5000/api/orders/" + orderO.id;
        var ordJSN = {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: orderO.id,
                adressOrigin: orderO.adressOrigin,
                deadline: orderO.deadline,
                adressDestination: orderO.adressDestination,
                receiverName: orderO.receiverName,
                addNote: orderO.addNote,
                delivery_ID_FK: orderO.delivery_ID_FK,
                customer_ID_FK: orderO.customer_ID_FK,
                status_ID_FK: orderO.status_ID_FK,
                courier_ID_FK: orderO.courier_ID_FK,
            })
        }
        fetch(url, ordJSN);
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
        var orderDelivery_ID_FK;
        if (this.state.delivery_ID_FK !== null && this.state.delivery_ID_FK!==undefined)
            orderDelivery_ID_FK = parseInt(this.state.delivery_ID_FK);
        else
            orderDelivery_ID_FK = null;

        var orderCustomer_ID_FK = this.state.customer_ID_FK;
        var orderStatus_ID_FK = parseInt(this.state.status_ID_FK);
        var orderCourier_ID_FK = this.state.courier_ID_FK;


        if (!orderAdressOrigin || !orderDeadline || !orderAdressDestination || !orderReceiverName || !orderCustomer_ID_FK || orderStatus_ID_FK <= 0 || !orderCourier_ID_FK) {
            return;
        }
        var formDeadline = new Date(orderDeadline);
        if (this.props.orderID === undefined)
            this.CreateOrder({ adressOrigin: orderAdressOrigin, deadline: formDeadline, adressDestination: orderAdressDestination, receiverName: orderReceiverName, addNote: orderAddNote, delivery_ID_FK: orderDelivery_ID_FK, customer_ID_FK: orderCustomer_ID_FK, status_ID_FK: orderStatus_ID_FK, courier_ID_FK: orderCourier_ID_FK });
        else
            this.EditOrder({ id: this.props.orderID, adressOrigin: orderAdressOrigin, deadline: formDeadline, adressDestination: orderAdressDestination, receiverName: orderReceiverName, addNote: orderAddNote, delivery_ID_FK: orderDelivery_ID_FK, customer_ID_FK: orderCustomer_ID_FK, status_ID_FK: orderStatus_ID_FK, courier_ID_FK: orderCourier_ID_FK });
        document.location.href = "/";
    }

    render() {
        
        var deadline, adressOrigin, adressDestination, receiverName, addNote, delivery_ID_FK, customer_ID_FK, status_ID_FK, courier_ID_FK;
        if (this.props.orderID === undefined || this.state.order === null) {
            deadline = new Date(Date.now()).toISOString().split('T')[0]; adressOrigin = ""; adressDestination = ""; receiverName = ""; addNote = ""; delivery_ID_FK = ""; customer_ID_FK = ""; status_ID_FK = ""; courier_ID_FK = "";
        }
        else {
            deadline = this.state.order.deadline.toString().substring(0, 10);
            adressOrigin = this.state.order.adressOrigin;
            adressDestination = this.state.order.adressDestination;
            receiverName = this.state.order.receiverName;
            addNote = this.state.order.addNote;
            customer_ID_FK = this.state.order.customer_ID_FK;
            status_ID_FK = this.state.order.status_ID_FK.toString();
            courier_ID_FK = this.state.order.courier_ID_FK;
            if (this.state.order.delivery_ID_FK !== null)
                delivery_ID_FK = this.state.order.delivery_ID_FK.toString()
            else
                delivery_ID_FK = ""


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

                                                    <Grid item xs={12} sm={6}>
                                                        <TextField
                                                            id="delivery_ID_FK"
                                                            label="Код доставки"
                                                            fullWidth
                                                            defaultValue={delivery_ID_FK}
                                                            onChange={this.onDelivery_ID_FKChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12} sm={6}>
                                                        <TextField
                                                            id="customer_ID_FK"
                                                            required
                                                            label="Код получателя"
                                                            fullWidth
                                                            defaultValue={customer_ID_FK}
                                                            onChange={this.onCustomer_ID_FKChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12} sm={6}>
                                                        <TextField
                                                            id="status_ID_FK"
                                                            required
                                                            label="Статус"
                                                            fullWidth
                                                            defaultValue={status_ID_FK}
                                                            onChange={this.onStatus_ID_FKChange}
                                                        />
                                                    </Grid>

                                                    <Grid item xs={12} sm={6}>
                                                        <TextField
                                                            id="courier_ID_FK"
                                                            required
                                                            label="Код курьера"
                                                            fullWidth
                                                            defaultValue={courier_ID_FK}
                                                            onChange={this.onCourier_ID_FKChange}
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

