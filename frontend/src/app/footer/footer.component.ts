import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  constructor() {}

  openSocial(platform: string) {
    const urls: Record<string, string> = {
      facebook: "https://facebook.com",
      twitter: "https://twitter.com",
      instagram: "https://instagram.com",
      youtube: "https://youtube.com",
    };
    window.open(urls[platform], "_blank");
  }
}

