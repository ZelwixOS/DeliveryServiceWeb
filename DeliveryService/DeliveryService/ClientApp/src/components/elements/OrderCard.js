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
    axios.delete(url).then((response) => DRInfoOrder(response));
  }
  
  function Confirmed() {
    const url = comUrl + "/api/Confirmed/" + orderContent.id;
    axios.post(url).then(UpdateOrderInfo);
  }

  function Recieved() {
    var url = comUrl + "/api/Recieved/" + orderContent.id
    axios.post(url).then(props.listUpdate);
  }

  function Delivered() {
    var url = comUrl + "/api/Delivered/" + orderContent.id
    axios.post(url).then(UpdateOrderInfo);
  }

  function Add() {
    var url = comUrl + "/api/Add/" + orderContent.id;
    axios.post(url).then(props.listUpdate);
  }

  function DRInfoOrder(resp) {
    var msg = "";
    if (resp.status === 401) {
      msg = "У вас не хватает прав для удаления";
    } else if (resp.status === 200) {
      props.listUpdate();
    } else {
      msg = "Неизвестная ошибка";
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
          Заказ {orderContent.id}
        </Typography>
        <List component="nav" className={classes.root} aria-label="Заказ">
          <ListItem >

            <ListItemText primary={"Стоимость: " + orderContent.cost} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"Начальный адрес: " + orderContent.adressOrigin} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"Дата заказа: " + orderContent.orderDateS} />
          </ListItem>
          <ListItem >

            <ListItemText primary={"Адрес доставки: " + orderContent.adressDestination} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"Крайний срок: " + orderContent.deadlineS} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"Имя получателя: " + orderContent.receiverName} />
          </ListItem>
          <Divider />
          {
            orderContent.addNote !== null
            &&
            <React.Fragment>
              <ListItem >
                <ListItemText primary={"Примечание: " + orderContent.addNote} />
              </ListItem>
              <Divider />
            </React.Fragment>

          }

          {
            orderContent.delivery_ID_FK !== null
            &&
            <React.Fragment>
              <ListItem >
                <ListItemText primary={"Код доставки: " + orderContent.delivery_ID_FK} />
              </ListItem>
              <Divider />
            </React.Fragment>

          }


          <ListItem >
            <ListItemText primary={"Заказчик: " + orderContent.customerS} />
          </ListItem>
          <Divider />
          <ListItem >

            <ListItemText primary={"Статус: " + orderContent.status.statusName} />
          </ListItem>
          <Divider />
          <ListItem >
            {
              orderContent.courierS !== null
                ?
                <ListItemText primary={"Курьер: " + orderContent.courierS} />
                :
                <ListItemText primary={"Курьер: не назначен"} />
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
                <Button size="small" onClick={UpdateOrder}> Редактировать </Button>
                <Button size="small" onClick={DeleteOrder} >Удалить </Button>
              </React.Fragment>
              :
              orderContent.status_ID_FK === 5
                ?
                <React.Fragment>
                  <Button size="small" onClick={Confirmed} > Подтвердить </Button>
                  <Button size="small" onClick={UpdateOrder}> Редактировать </Button>
                <Button size="small" onClick={DeleteOrder} >Удалить </Button>
                </React.Fragment>
                :
                orderContent.status_ID_FK === 3 || orderContent.status_ID_FK === 4
                  ?
                  <React.Fragment>
                    <Button size="small" onClick={Recieved} > Получил </Button>
                  </React.Fragment>
                  :
                  <React.Fragment />
            :
            role === "courier"
              ?
              orderContent.status_ID_FK === 3
                ?
                <Button size="small" onClick={Delivered}> Доставил </Button>
                :
                cardType === "available"
                  ?
                  <Button size="small" onClick={Add}> Забронировать </Button>
                  :
                  <React.Fragment />
              :
              <React.Fragment />
        }

      </CardActions>
    </Card>
  );
}