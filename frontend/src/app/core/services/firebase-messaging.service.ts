import { Injectable } from '@angular/core';
import { initializeApp } from 'firebase/app';
import { getMessaging, getToken, onMessage, Messaging } from 'firebase/messaging';
import { environment } from '../../../enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class FirebaseMessagingService {
  private messaging: Messaging;

  constructor() {
    const app = initializeApp(environment.firebase);
    this.messaging = getMessaging(app);
  }

  async requestPermissionAndGetToken(): Promise<string | null> {
    try {
      const permission = await Notification.requestPermission();

      if (permission !== 'granted') {
        console.warn('Notification permission not granted.');
        return null;
      }

      const token = await getToken(this.messaging, {
        vapidKey: environment.firebase.vapidKey
      });

      console.log('FCM token:', token);
      return token;
    } catch (error) {
      console.error('Error getting FCM token:', error);
      return null;
    }
  }

  listenForMessages(): void {
    onMessage(this.messaging, payload => {
      console.log('Foreground message received:', payload);
    });
  }
}