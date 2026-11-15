import { ValidationMessageComponent } from '../validation-message/validation-message.component';
import { PlayStationClient } from '../../shared/clients/games/playstation.client';
import { npsso, NPSSO_REGEX } from '../../shared/helpers/validation.helpers';
import { FormRoot, FormField, FieldTree } from '@angular/forms/signals';
import { Component, inject, model, signal } from '@angular/core';
import { signalForm } from '../../shared/helpers/form.helpers';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'link-playstation',
    standalone: true,
    imports: [ReactiveFormsModule, FormRoot, FormField, ValidationMessageComponent],
    styleUrl: './link-playstation.component.css',
    templateUrl: './link-playstation.component.html',
})
export class LinkPlayStationComponent {
    private readonly playStationClient = inject(PlayStationClient);

    public isLinkMethodSecure = signal<boolean>(false);
    public isOpen = model.required<boolean>();

    public form = signalForm<{ onlineId: string, npsso: string }>({
        onlineId: "",
        npsso: ""
    }, (schema) => {
        npsso(schema.npsso);
    }, { submission: { action: async (fieldtree) => this.search(fieldtree), ignoreValidators: 'all' } });

    public setNPSSO(event: Event) {
        const target = event.target as HTMLInputElement | null;
        const json = target?.value.trim();
        if (!target || !json || NPSSO_REGEX.test(json))
            return;

        try {
            const object = JSON.parse(json);
            if (typeof object?.npsso !== 'string' || !NPSSO_REGEX.test(object.npsso))
                return;
                
            this.form.field.npsso().value.set(object.npsso);
            target.value = object.npsso;
        } catch { }
    }

    public async search(fieldtree: FieldTree<{ onlineId: string, npsso: string }>) {
        const result = this.isLinkMethodSecure()
            ? await this.playStationClient.getUserByNpsso({ npsso: fieldtree.npsso().value() })
            : await this.playStationClient.getUserByOnlineId({ onlineId: fieldtree.onlineId().value() });

        console.log(result);
    }

    public switch(secure: boolean) {
        this.isLinkMethodSecure.set(secure);
        this.form.field().reset();
        this.form.field().value.set({ npsso: "", onlineId: "" });
    }

    public close() {
        this.isOpen.set(false);
        this.isLinkMethodSecure.set(false);
        this.form.field().reset();
        this.form.field().value.set({ npsso: "", onlineId: "" });
    }
}
