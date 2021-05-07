import React from 'react'
import { Box, Container, Grid, Typography, Button } from '@material-ui/core'
import CssBaseline from '@material-ui/core/CssBaseline'
import Navbar from '../minmod/Navbar'
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';

export default function ForGuests() {
    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <Navbar title={"Emu"}>  </Navbar>
            <Box >
                <Grid container spacing={1}>
                    <Grid>
                        <Card variant="outlined">
                            <CardContent>
                                <Typography variant="h4" gutterBottom>
                                    Для новых пользователей 
                                </Typography>
                                <Typography variant="h6">
                                    Служба доставок Emu - лучший выбор для тех, кто хочет как доставить что-либо из точки А в точку Б!
                                </Typography>
                                <Typography variant="h6">
                                    Для того, чтобы воспользоваться нашими услугами, просто зарегистрируйтесь на нашем сайте, нажав на кнопку:
                                </Typography>
                                <Button
                                        variant="contained"
                                        color="secondary"
                                        href="/registration"
                                        endIcon={<ArrowForwardIosIcon/>}
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
