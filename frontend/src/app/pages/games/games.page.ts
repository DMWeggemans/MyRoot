import { HeaderComponent } from '../../components/header/header.component';
import { ArrowRightIcon } from '../../icons/arrow-right/arrow-right.icon';
import { ArrowLeftIcon } from '../../icons/arrow-left/arrow-left.icon';
import { Component, computed, signal } from '@angular/core';
import { OwnedIcon } from '../../icons/owned/owned.icon';
import { InfoIcon } from '../../icons/info/info.icon';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-games.page',
  standalone: true,
  imports: [ArrowLeftIcon, ArrowRightIcon, OwnedIcon, InfoIcon, HeaderComponent, RouterLink],
  templateUrl: './games.page.html',
  styleUrl: './games.page.css',
})
export class GamesPage {
  public pageSize = signal<number | null>(12);

  public items = computed(() => Array.from({ length: this.pageSize() ?? 0 }, (_, index) => ({
    id: `${index + 1}`,
    title: "This is a super really big game title.",
    owned: 533952,
    image: "https://www.minecraft.net/content/dam/minecraftnet/games/minecraft/key-art/Homepage_Collectables-Carousel-H_Java-Bedrock_414x414_01.jpg"
  })));

  public setPageSize(number: number) {
    const pageSize = this.pageSize() !== number
      ? (!Number.isNaN(number)
        ? number
        : 12)
      : null;
    this.pageSize.set(pageSize);
  }

  public setCustomPageSize(event: Event) {
    const target = event.target as HTMLElement;
    const isOpen = target.innerHTML === "...";
    if (!isOpen) return;

    target.innerHTML = "";
    const element = document.createElement('input');
    element.type = "text";
    element.classList.add("page-size-input");

    const handleBlur = (event: Event) => {
      const target = event.target as HTMLInputElement;
      if (!target.parentElement) return;
      target.parentElement.innerHTML = "...";
    };

    element.addEventListener('keydown', (event: KeyboardEvent) => {
      if (event.key !== 'Enter') return;

      const target = event.target as HTMLInputElement;
      if (!target.parentElement) return;

      const value = Number(target.value.trim() || NaN);
      this.setPageSize(value);
      target.removeEventListener('blur', handleBlur);
      target.parentElement.innerHTML = "...";
    });

    element.addEventListener('blur', handleBlur);

    target.appendChild(element);
    element.focus();
  }
}
