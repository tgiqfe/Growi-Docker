import React from "react";
import { Container, Box, Typography, Paper } from "@mui/material";
import ControlButton from "./ControlButton";

export default () => {
  return (
    <Container maxWidth="sm">
      <Paper
        elevation={3}
        sx={{
          mt: 8,
          px: 2,
          py: 4,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h4">
          COCKPIT
        </Typography>
        <Box>
          <ControlButton
            method="Get"
            name="test"
            text="Test"
            apiUrl="/api/script/test01.sh"
            body = ""
          />
          <ControlButton
            method="Get"
            name="Backup"
            text="MongoDB Backup."
            apiUrl="/api/script/backup_mongodb.sh"
            body = ""
          />
          <ControlButton
            method="Get"
            name="Restore"
            text="MongoDB Restore. (latest version.)"
            apiUrl="/api/script/restore_mongodb.sh"
            body = ""
          />
          <ControlButton
            method="Get"
            name="Export"
            text="Export revision from Growi."
            apiUrl="/api/mongodb/export?server=mongo&port=27017&name=growi"
            body = ""
          />
          <ControlButton
            method="Post"
            name="Export2"
            text="Export revision from Growi."
            apiUrl="/api/mongodb/export"
            body={JSON.stringify({
              dbServer : "mongo",
              dbPort: 27017,
              dbName: "growi",
              outputDir: "/tmp/mongoExport"
            })}
          />
        </Box>
      </Paper>
    </Container>
  );
};
