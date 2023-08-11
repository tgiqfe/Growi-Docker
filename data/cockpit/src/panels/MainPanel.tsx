import React from "react";
import { Container, Box, Typography } from "@mui/material";
import ControlButton from "./ControlButton";

export default () => {
  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h4">
          COCKPIT
        </Typography>
        <ControlButton
          name="test"
          text="sample"
          apiUrl="/api/script/test01.sh"
        />
      </Box>
    </Container>
  );
};
