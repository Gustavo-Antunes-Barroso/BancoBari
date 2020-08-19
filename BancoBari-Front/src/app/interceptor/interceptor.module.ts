import { Injectable, NgModule } from '@angular/core';
import { Observable } from 'rxjs';
import {
 HttpEvent,
 HttpInterceptor,
 HttpHandler,
 HttpRequest,
} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { environment } from "src/environments/environment";

@Injectable()

export class HttpsRequestInterceptor implements HttpInterceptor {
 intercept(
  req: HttpRequest<any>,
  next: HttpHandler,
 ): Observable<HttpEvent<any>> {
  const dupReq = req.clone({
      url: `${environment.api}/${req.url}`
 });
 return next.handle(dupReq);
 }
}
    
    
@NgModule({
providers: [
 {
  provide: HTTP_INTERCEPTORS,
  useClass: HttpsRequestInterceptor,
  multi: true,
 },
],
})
    
    
export class Interceptor {}