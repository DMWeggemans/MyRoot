import { Injectable, signal } from "@angular/core";

@Injectable()
export class GameStateService {
    public game = signal<any>(null);
}