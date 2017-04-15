/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Volha
 */
// MyTest.java test class for JNI experimentation 
// example 2

public class MyTest {
     private static int magic_counter=777;

     public static void mymain() {   // <=== We will call this 
         System.out.println("Hello, World in java from mymain");
         System.out.println(magic_counter);
     }
}