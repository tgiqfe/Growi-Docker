import React from "react";
import { Box, TextField, Button, Stack, Typography } from "@mui/material";

type Props = {
    name: string;
    text: string;
    apiUrl: string;
};

const TestEvent = () => {
    alert("ok");
};

export default (props: Props) => {
    return (
        <>
            <Stack direction="row" sx={{ mt: 3, mb: 2 }}>
                <Button
                    variant="contained"
                    onClick={() => {
                        alert("okk");
                        fetch(props.apiUrl)
                            .then((res) => res.json())
                            .then((json) => console.log(json));
                    }}
                >
                    {props.name}
                </Button>
                <Typography variant="h5" sx={{ padding: 1 }}>
                    {props.text}
                </Typography>
            </Stack>
        </>
    );
};
