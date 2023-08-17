import React from "react";
import { Box, TextField, Button, Stack, Typography } from "@mui/material";

type Props = {
  method: string;
  name: string;
  text: string;
  apiUrl: string;
  body: string;
};

export default (props: Props) => {
  return (
    <>
      <Stack direction="row" sx={{ mt: 2, mb: 1 }}>
        <Button
          variant="contained"
          onClick={() => {
            if (props.method == "Get") {
              fetch(props.apiUrl)
                .then((res) => res.json())
                .then((json) => console.log(json));
            } else if (props.method == "Post") {
              fetch(props.apiUrl, {
                method: "POST",
                headers: {
                  "Content-Type": "application/json",
                },
                body: props.body,
              })
                .then((res) => res.json())
                .then((json) => console.log(json));
            }
          }}
          sx={{
            width: 100,
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
