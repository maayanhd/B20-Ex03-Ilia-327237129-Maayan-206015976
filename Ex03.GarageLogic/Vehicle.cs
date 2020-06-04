﻿using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Ex03.GarageLogic
{
     public abstract class Vehicle
     {
          private readonly string r_LicenseNum;
          private string m_Model;
          private float m_EnergyLeftInPercents;
          private readonly List<Wheel> r_Wheels;
          protected List<string> m_MemberInfoStr;

          protected Vehicle(string i_LicenseNumber)
          {
              r_LicenseNum= i_LicenseNumber;
              r_Wheels = new List<Wheel>();
               ManageMemberInfo();
          }

          public virtual void ManageMemberInfo()
          {
               m_MemberInfoStr = new List<string> { "License Number", "Type Of Vehicle", "A model" };
          }

          public virtual bool IsCurrentMemberValid(int i_NumOfField, string i_InputStr)
          {
               bool isMemberValid = false;
               int io_NumOfWheelBasedOnField = i_NumOfField;

               i_NumOfField = GetNumFieldForWheelsMember(i_NumOfField, ref io_NumOfWheelBasedOnField);

               switch (i_NumOfField)
               {
                    case 1:
                         isMemberValid = IsLicenseNumValid(i_InputStr);
                         break;
                    case 2:
                         isMemberValid = int.TryParse(i_InputStr, out int option) == true ? IsTypeOfVehicleValid(option) : false; 
                         break;
                    case 3:
                         isMemberValid = IsModelValid(i_InputStr);
                         break;
                    case 4:
                         isMemberValid = Wheels[io_NumOfWheelBasedOnField].IsManufactorerValid(i_InputStr);
                         break;
                    case 5:
                         isMemberValid = float.TryParse(i_InputStr, out float o_AirPressure) == true ? Wheels[io_NumOfWheelBasedOnField].IsAirPressureIsValid(o_AirPressure) : false;
                         break;
                         // vehicle ends in case 3 + 2 * numofwheels  
               }

               return isMemberValid;

          }

          public int GetNumFieldForWheelsMember(int i_CaseNum, ref int io_NumOfWheelBasedOnField)
          {
               if (i_CaseNum >= 4 && i_CaseNum <= 4 + r_Wheels.Count)
               {
                    i_CaseNum = i_CaseNum % 2 == 0 ? 4 : 5;
                    io_NumOfWheelBasedOnField = io_NumOfWheelBasedOnField - 4 / 2;
               }
                             
               return i_CaseNum;
          }

          //****************Validations Methods******************//  

          public bool IsTypeOfVehicleValid(int i_TypeOfVehicle)
          {
               return i_TypeOfVehicle > 0 && i_TypeOfVehicle <= Instance.r_VehicleTypesStr.Count;
          }

          public bool IsEnergyLeftValid(float i_EnergyLeftInPercents)
          {
               return i_EnergyLeftInPercents <= 100 && i_EnergyLeftInPercents >= 0;
          }

          public bool IsLicenseNumValid(string i_LicenseNum)
          {
               bool isValid = false;
               int licenceNumInt;

               if (i_LicenseNum.Length == 8)
               {
                    isValid = true;
                    foreach (char ch in i_LicenseNum)
                    {
                         if (char.IsDigit(ch) == false)
                         {
                              isValid = false;
                              break;
                         }
                    }
               }

               return isValid;

          }

          public bool IsModelValid(string i_Model)
          {
              bool isValid = false;

              foreach(char ch in i_Model)
              {
                  isValid = char.IsLetterOrDigit(ch);

                  if(isValid == false)
                  {
                      break;
                  }

              }

              return isValid;

          }

          //***********Override of Object Methods*********//  
          public override string ToString()
          {
              string vehicleString = string.Format(
                  "Model: {0}{1}Lisence Number: {2}",
                  Model,
                  Environment.NewLine,
                  LicenseNum);

              return vehicleString;
          }

          public override int GetHashCode()
          {
               return int.Parse(r_LicenseNum);
          }
          
          public override bool Equals(object i_obj)
          {
              Vehicle vehicleToCompare = i_obj as Vehicle;
              bool isEqual = false;

              if(vehicleToCompare != null)
              {
                  isEqual = this.LicenseNum.Equals(vehicleToCompare.LicenseNum);
              }
              else
              {
                  throw new ArgumentException();
              }

              return isEqual;
          }

          //****************Properties******************//  

          public string LicenseNum
          {
               get
               {
                    return r_LicenseNum;
               }

          }

          public string Model
          {
               get
               {
                    return m_Model;
               }

          }

          public float EnergyLeftInPercents
          {
               get
               {
                    return m_EnergyLeftInPercents;
               }

               set
               {
                    m_EnergyLeftInPercents = value;
               }

          }

          public List<Wheel> Wheels
          {
               get
               {
                    return r_Wheels;
               }

          }

          public List<string> MemberInfoStrings
          {
               get
               {
                    return m_MemberInfoStr;
               }

          }

     }

}
