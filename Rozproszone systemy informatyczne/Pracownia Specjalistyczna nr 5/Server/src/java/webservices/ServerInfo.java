/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.jg.rsi;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.HandlerChain;

@WebService
@HandlerChain(file="handler-chain.xml")
public class ServerInfo {

    @WebMethod
    public String getInfo(){
        return "moj serwer";
    }
}
