import { setupZoneTestEnv } from 'jest-preset-angular/setup-env/zone';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

setupZoneTestEnv();
registerLocaleData(localePt, 'pt');
