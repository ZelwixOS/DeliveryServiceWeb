import React from 'react';
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
  const classes = useStyles();
  const orderContent = props.orderContent;
  const cardType = props.type;
  const role = props.role;

  function DeleteOrder() {
    const url = comUrl + "/api/orders/" + orderContent.id;
    axios.delete(
      url, { withCredentials: true }
    ).then((response) => DRInfoOrder(response));
  }

  function DRInfoOrder(resp) {
    var msg = "";
    if (resp.status === 401) {
      msg = "У вас не хватает прав для удаления";
    } else if (resp.status === 201) {

      document.location.href = "/";

    } else {
      msg = "Неизвестная ошибка";
    }
    document.querySelector("#actionMsg").innerHTML = msg;
  }

  return (
    <Card className={classes.root} variant="outlined" style={ cardType==="past" ? {background: "#DDD"}: {}}>
      <CardContent>
        <div id="actionMsg" style={{ color: "#F00" }} />
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
            <ListItemText primary={"Получатель: " + orderContent.customerS} />
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
          <OrderItemList orderID={orderContent.id} type={cardType} role={role} status = {orderContent.status_ID_FK}/>



        </List>
      </CardContent>
      <CardActions>

        {
          role === "customer"
            ?
            orderContent.status_ID_FK === 1
              ?
              <React.Fragment>
                <Button size="small" href={"/orderForm/" + orderContent.id}> Редактировать </Button>
                <Button size="small" onClick= {axios.post(comUrl + "/api/OneClick/Recieved/", {withCredentials: true})} > Получил </Button>
                <Button size="small" onClick={DeleteOrder} >Удалить </Button>
              </React.Fragment>
              :
              <React.Fragment />
            :
            role === "courier"
              ?
              cardType === "active"
                ?
                <Button size="small" onClick= {axios.post(comUrl + "/api/OneClick/Delivered/" + orderContent.id, {withCredentials: true})}> Доставил </Button>
                :
                cardType === "available"
                  ?
                  <Button size="small" onClick= {axios.post(comUrl + "/api/OneClick/Add/" + orderContent.id, {withCredentials: true})}> Забронировать </Button>
                  :
                  <React.Fragment />
              :
              <React.Fragment/>
        }

      </CardActions>
    </Card>
  );
}