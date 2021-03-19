import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'app-breadcrumbs',
    templateUrl: './tuiBreadcrumbs.component.html',
    styleUrls: ['./../../../../assets/scss/layout/__breadscrumbs.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TuiBreadcrumbs {
    items = [
        {
            caption: 'Главная',
            routerLink: '/tui-select',
        },
        {
            caption: 'Категории',
            routerLink: '/tui-multi-select',
        },
        {
            caption: 'Транспорт',
            routerLink: '/tui-multi-select',
        },
        {
            caption: 'Автомобили',
            routerLink: '/tui-breadcrumbs',
            routerLinkActiveOptions: { exact: true },
        },
    ];
}