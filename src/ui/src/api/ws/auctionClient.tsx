import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';

// TODO: Use this logic as example for future SignalR interactions
const AuctionMessaging: React.FC = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_GATEWAY_URL!}/messaging-auction`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.on('BidUpdated', (productId: string, newPrice: number) => {
        console.log(
          'Received from AuctionHub productId and newPrice:',
          productId,
          newPrice
        );
      });
    }
  }, [connection]);

  const sendMessage = async () => {
    if (connection) {
      try {
        await connection.start();

        await connection.invoke(
          'UpdateBid',
          '44913A7E-30DB-45B0-A8E5-26B0A727A71B',
          12345
        );

        await connection.stop();
      } catch (error) {
        console.error(error);
      }
    }
  };

  return (
    <div>
      <button onClick={sendMessage}>Send Message</button>
    </div>
  );
};

export default AuctionMessaging;
