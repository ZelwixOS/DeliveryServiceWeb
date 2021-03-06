import React, { useState } from 'react';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import { Button, Typography } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import OrderItemList from "../elements/OrderItemList";
import axios from 'axios';

const comUrl = "http://localhost:5000"

const useStyles = makeStyles((theme = useTheme()) => ({
  root: {
    marginBottom: theme.spacing(2)
  },
}));

axios.defaults.withCredentials = true

export default function OrderCard(props) {

  const [orderContent, setOrderContent] = useState(props.orderContent);
  const [message, setMessage] = useState("");

  const classes = useStyles();
  const cardType = props.type;
  const role = props.role;

  function UpdateOrderInfo()
  {
    const url = comUrl + "/api/orders/" + orderContent.id;
    axios.get(url).then((response) => setOrderContent(response.data));
  }

  function UpdateOrder(){
    const url = comUrl + "/api/Updating/" + orderContent.id;
    axios.post(url).then(document.location.href = "/orderForm/" + orderContent.id);
  }

  function DeleteOrder() {
    const url = comUrl + "/api/DeleteOrder/" + orderContent.id;
    axios.delete(url).then((response) => DRInfoOrder(response)).then(props.listUpdate);;
  }
  
  function Confirmed() {
    const url = comUrl + "/api/Confirmed/" + orderContent.id;
    axios.post(url).then((response) => DRInfoOrder(response)).then(UpdateOrderInfo);
  }

  function Recieved() {
    var url = comUrl + "/api/Recieved/" + orderContent.id
    axios.post(url).then((response) => DRInfoOrder(response)).then(props.listUpdate);
  }

  function Delivered() {
    var url = comUrl + "/api/Delivered/" + orderContent.id
    axios.post(url).then((response) => DRInfoOrder(response)).then(UpdateOrderInfo);
  }

  function Add() {
    var url = comUrl + "/api/Add/" + orderContent.id;
    axios.post(url).then((response) => DRInfoOrder(response)).then(props.listUpdate);
  }

  function DRInfoOrder(resp) {
    var msg = resp.data;
    if (resp.status === 401) {
      msg = "?? ?????? ???? ?????????????? ????????";
    } else if (resp.status !== 200 && resp.status !== 204) {
      msg = "?????????????????????? ????????????";
    }
    setMessage(msg);
  }

  return (
    <Card className={classes.root} variant="outlined" style={cardType === "past" ? { background: "#DDD" } : orderContent.status_ID_FK === 5 ? {background: "#EA0"}: {}}>
      <CardContent>
        {
          message !== "" &&
        <div id="actionMsg" style={{ color: "#F00" }} > {message} </div>
        }
        
        <Typography className={classes.title} gutterBottom>
          ?????????? {orderContent.id}
        </Typography>
        <List component="nav" className={classes.root} aria-label="??????????">
          <ListItem >

            <ListItemText primary={"??????????????????: " + orderContent.cost} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"?????????????????? ??????????: " + orderContent.adressOrigin} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"???????? ????????????: " + orderContent.orderDateS} />
          </ListItem>
          <ListItem >

            <ListItemText primary={"?????????? ????????????????: " + orderContent.adressDestination} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"?????????????? ????????: " + orderContent.deadlineS} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"?????? ????????????????????: " + orderContent.receiverName} />
          </ListItem>
          <Divider />
          {
            orderContent.addNote !== null
            &&
            <React.Fragment>
              <ListItem >
                <ListItemText primary={"????????????????????: " + orderContent.addNote} />
              </ListItem>
              <Divider />
            </React.Fragment>

          }

          {
            orderContent.delivery_ID_FK !== null
            &&
            <React.Fragment>
              <ListItem >
                <ListItemText primary={"?????? ????????????????: " + orderContent.delivery_ID_FK} />
              </ListItem>
              <Divider />
            </React.Fragment>

          }


          <ListItem >
            <ListItemText primary={"????????????????: " + orderContent.customerS} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"????????????: " + orderContent.status.statusName} />
          </ListItem>
          <Divider />
          <ListItem >
            {
              orderContent.courierS !== null
                ?
                <ListItemText primary={"????????????: " + orderContent.courierS} />
                :
                <ListItemText primary={"????????????: ???? ????????????????"} />
            }
          </ListItem>
          <Divider />
          <OrderItemList orderID={orderContent.id} type={cardType} role={role} status={orderContent.status_ID_FK} updateOrder={UpdateOrderInfo}/>



        </List>
      </CardContent>
      <CardActions>

        {
          role === "customer"
            ?
            orderContent.status_ID_FK === 1
              ?
              <React.Fragment>
                <Button size="small" onClick={UpdateOrder}> ?????????????????????????? </Button>
                <Button size="small" onClick={DeleteOrder} >?????????????? </Button>
              </React.Fragment>
              :
              orderContent.status_ID_FK === 5
                ?
                <React.Fragment>
                  <Button size="small" onClick={Confirmed} > ?????????????????????? </Button>
                  <Button size="small" onClick={UpdateOrder}> ?????????????????????????? </Button>
                <Button size="small" onClick={DeleteOrder} >?????????????? </Button>
                </React.Fragment>
                :
                orderContent.status_ID_FK === 3 || orderContent.status_ID_FK === 4
                  ?
                  <React.Fragment>
                    <Button size="small" onClick={Recieved} > ?????????????? </Button>
                  </React.Fragment>
                  :
                  <React.Fragment />
            :
            role === "courier"
              ?
              orderContent.status_ID_FK === 3
                ?
                <Button size="small" onClick={Delivered}> ???????????????? </Button>
                :
                cardType === "available"
                  ?
                  <Button size="small" onClick={Add}> ?????????????????????????? </Button>
                  :
                  <React.Fragment />
              :
              <React.Fragment />
        }

      </CardActions>
    </Card>
  );
}