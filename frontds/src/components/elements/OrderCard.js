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

const useStyles = makeStyles((theme = useTheme()) => ({
  root: {
    marginBottom: theme.spacing(2)
  },

}));


export default function CourseCard(props) {
  const classes = useStyles();
  const orderContent = props.orderContent;

  function DeleteOrder()
  {
    let response = fetch("http://localhost:5000/api/orders/" + orderContent.id, { method: 'DELETE' });

    var msg = "";
    if (response.status === 401) {
    msg = "У вас не хватает прав для создания";
    } else if (response.status === 201) {
 
        document.location.href = "/";

    } else {
    msg = "Неизвестная ошибка";
    }

    document.querySelector("#actionMsg").innerHTML = msg;
   
  }

  return (
    <Card className={classes.root} variant="outlined" >
      <CardContent>
        <div id="actionMsg" style={{color: "#F00"}}/>
        <Typography className={classes.title} gutterBottom>
          Заказ {orderContent.id}
        </Typography>
        <List component="nav" className={classes.root} aria-label="Заказ">
          <ListItem >
            
              <ListItemText primary={"Стоимость: "+orderContent.cost} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Начальный адрес: "+orderContent.adressOrigin} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Дата заказа: "+orderContent.orderDateS} />
          </ListItem>
          <ListItem >
            
              <ListItemText primary={"Адрес доставки: "+orderContent.adressDestination} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Крайний срок: "+orderContent.deadlineS} />
          </ListItem>
          <Divider />
          <ListItem >
            
              <ListItemText primary={"Имя получателя: "+orderContent.receiverName} />
          </ListItem>
          <Divider />
{
  orderContent.addNote!=null
  ?
  <React.Fragment>
     <ListItem >
          <ListItemText primary={"Примечание: "+orderContent.addNote} />
      </ListItem>
      <Divider />
  </React.Fragment>
      :

      <div></div>
}

{
  orderContent.delivery_ID_FK!=null
  ?
  <React.Fragment>
          <ListItem >
            <ListItemText primary={"Код доставки: "+orderContent.delivery_ID_FK} />
        </ListItem>
        <Divider />
  </React.Fragment>
      :
  <div></div>
}


          <ListItem >
              <ListItemText primary={"Получатель: " + orderContent.customerS} />
          </ListItem>
          <Divider />
          <ListItem >

              <ListItemText primary={"Статус: "+orderContent.status.statusName} />
          </ListItem>
          <Divider />
          <ListItem >

              <ListItemText primary={"Курьер: " + orderContent.courierS} />
          </ListItem>
          <Divider />

          <OrderItemList orderID={orderContent.id}/>



        </List>
      </CardContent>
      <CardActions>
        <Button size="small" href={"/orderForm/" + orderContent.id}> Редактировать </Button>
        <Button size="small" onClick= { DeleteOrder} >
          Удалить </Button>
      </CardActions>
    </Card>
  );
}