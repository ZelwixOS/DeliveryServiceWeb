import React from "react";
import OrderForm from '../pages/OrderForm';
import {useParams} from "react-router-dom"; 


export default function OrderFormRedir() {
    let { slug } = useParams();
    return <OrderForm  orderID={slug}/>
  } 